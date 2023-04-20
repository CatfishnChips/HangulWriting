using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class GestureController : MonoBehaviour
{
    #region Singleton

    public static GestureController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion
    
    //[Header("Touch Settings")]
    //[SerializeField] private float _tapTime; // Time needed for distinguishing Tap from Hold. !!! Currently not used !!!
    //[SerializeField] private float _slowTapTime; // !!! Currently not used !!!
    //[SerializeField] private float _holdTime; // Time needed for the touch to register as Hold. Used to distinguish Tap from Hold.

    #region Touch Variables

    [Header("Gesture Settings")]
    [SerializeField] private float _scoreTreshold = 0.25f; // Least amount of score required for a gesture to be recognized.
    private bool _isTouchBActive;
    private List<Vector2> _pointList = new List<Vector2>();
    private DollarRecognizer _recognizerDollar = new DollarRecognizer();

    [Header("UI References")]
    [SerializeField] private TMP_InputField _nameInput; // Temporary
    [SerializeField] private TextMeshProUGUI _nameText, _scoreText; // Temporary

    private TouchData _touchB;

    #endregion

    private void Start() 
    {
        InputManager.Instance.OnTouchBBegin += OnTouchBBegin;
        InputManager.Instance.OnTouchBStationary += OnTouchBStationary;
        InputManager.Instance.OnTouchBDrag += OnTouchBDrag;
        InputManager.Instance.OnTouchBEnd += OnTouchBEnd;

        ReadGesture();
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchBBegin -= OnTouchBBegin;
        InputManager.Instance.OnTouchBStationary -= OnTouchBStationary;
        InputManager.Instance.OnTouchBDrag -= OnTouchBDrag;
        InputManager.Instance.OnTouchBEnd -= OnTouchBEnd;
    }

    #region Touch

    private void OnTouchBBegin(Vector2 screenPosition, Vector3 worldPosition) 
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            _touchB = new TouchData();
            _touchB.HasMoved = false;
            _isTouchBActive = true;

            _touchB.ScreenStartPosition = screenPosition;
            _pointList.Clear();
            _pointList.Add(_touchB.ScreenStartPosition);
        }
        else{
            _isTouchBActive = false;
        }
    }

    private void OnTouchBStationary(Vector2 screenPosition, Vector3 worldPosition)
    {
        if (_touchB.State == TouchState.Drag){
            if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
                _pointList.Add(screenPosition);
            }
        }
        _touchB.State = TouchState.Hold;
    }

    private void OnTouchBDrag(InputEventParams inputEventDragParams) 
    {   
        _touchB.HasMoved = true;
        if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
            _pointList.Add(inputEventDragParams.ScreenPosition);
        }
        _touchB.State = TouchState.Drag;
    }

    private void OnTouchBEnd(InputEventParams inputEventParams) 
    {
        //float distance = Vector2.Distance(_touchB.InitialScreenPosition, inputEventParams.ScreenPosition);
        //Vector2 direction = (_touchB.InitialScreenPosition - inputEventParams.ScreenPosition).normalized;

        if (_touchB.HasMoved){
            if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
                _touchB.ScreenEndPosition = inputEventParams.ScreenPosition;
                _pointList.Add(inputEventParams.ScreenPosition);

                RecognizeGesture(out string Name, out float Score);

                if (Score >= _scoreTreshold) {
                    EventManager.Instance.OnGesture?.Invoke(Name, _touchB);
                }
            }  
        }
    }

    #endregion

    public void RecordGesture() 
    {   
        string name = _nameInput.text;
        _recognizerDollar.SavePattern(name, _pointList);
        Debug.Log("Gesture -  " + name + " has been recorded.");
    }

    public void WriteGesture() 
    {
        string name = _nameInput.text;
        DollarRecognizer.Unistroke unistroke = _recognizerDollar.SavePattern(name, _pointList);
        string gestureName = _nameInput.text;
        string fileName = string.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, gestureName, DateTime.Now.ToFileTime());
        GestureIO.WriteGesture(unistroke, gestureName, fileName);
        Debug.Log("Gesture -  " + name + " has been saved.");
    }

    private void ReadGesture() 
    {
        //Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("Gestures/");
		foreach (TextAsset gestureXml in gesturesXml)
			_recognizerDollar.AddToLibrary(GestureIO.ReadGestureFromXML(gestureXml.text));
            //Add loaded gestures to library.
    }

    private void RecognizeGesture(out string Name, out float Score) 
    {
        for (int i = 0; i < _pointList.Count; i++)
        {
            if (i-1 >= 0)
            Debug.DrawLine(new Vector3(_pointList[i-1].x, _pointList[i-1].y, 10), new Vector3(_pointList[i].x, _pointList[i].y, 10), Color.green, 3);
        }
        DollarRecognizer.Result result = _recognizerDollar.Recognize(_pointList);

        if (result.Match != null) {
            Name = result.Match.Name;
            Score = result.Score;

            _nameText.text = "Result: " + result.Match.Name.ToString();
            _scoreText.text = "Score: " + result.Score.ToString();
        }
        else 
        {
            Name = "";
            Score = 0;

            _nameText.text = "Result: No Result Found";
            _scoreText.text = "Score: 0";
        }
    }
}

public struct TouchData
{
    public Vector2 ScreenStartPosition;
    public Vector2 ScreenEndPosition;
    public bool HasMoved;
    public TouchState State;

    public TouchData(Vector2 screenStartPos, Vector2 screenEndPos, float timeOnScreen, float holdTime, bool hasMoved, TouchState state) => 
        (ScreenStartPosition, ScreenEndPosition, HasMoved, State) = (screenStartPos, screenEndPos, hasMoved, state);
}

public enum TouchState
{
    Hold,
    Drag
}
