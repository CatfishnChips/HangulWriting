using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using QDollarGestureRecognizer;

[RequireComponent(typeof(LineRenderer))]
public class MultiGestureController : MonoBehaviour
{
    #region Singleton

    public static MultiGestureController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _lineRenderer = GetComponent<LineRenderer>();
    }

    #endregion

    #region Touch Variables

    [Header("Gesture Settings")]
    [SerializeField] private float _scoreTreshold = 0.25f; // Least amount of score required for a gesture to be recognized.
    private int _currentStrokeIndex = 0;
    private bool _isTouchBActive;
    private List<PDollarGestureRecognizer.Point> _pointList = new List<PDollarGestureRecognizer.Point>();
    private List<PDollarGestureRecognizer.Gesture> _templateList = new List<PDollarGestureRecognizer.Gesture>();
    private DollarRecognizer _recognizerDollar = new DollarRecognizer();
    private LineRenderer _lineRenderer;
    private int _pointCount = 0;

    [Header("UI References")]
    [SerializeField] private TMP_InputField _nameInput; // Temporary
    [SerializeField] private TextMeshProUGUI _nameText, _scoreText; // Temporary

    private TouchData _touch;

    #endregion

    private QPointCloudRecognizer _recognizerQ = new QPointCloudRecognizer();

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
            _touch = new TouchData();
            _touch.HasMoved = false;
            _isTouchBActive = true;

            _touch.ScreenStartPosition = screenPosition;
            PDollarGestureRecognizer.Point point = new PDollarGestureRecognizer.Point(screenPosition.x, screenPosition.y, _currentStrokeIndex);
            _pointList.Add(point);
        }
        else{
            _isTouchBActive = false;
        }
    }

    private void OnTouchBStationary(Vector2 screenPosition, Vector3 worldPosition)
    {
        if (_touch.State == TouchState.Drag){
            if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
                PDollarGestureRecognizer.Point point = new PDollarGestureRecognizer.Point(screenPosition.x, screenPosition.y, _currentStrokeIndex);
                _pointList.Add(point);
                AddPointToLineRenderer(screenPosition);
            }
        }
        _touch.State = TouchState.Hold;
    }

    private void OnTouchBDrag(InputEventParams inputEventDragParams) 
    {   
        _touch.HasMoved = true;
        if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
            PDollarGestureRecognizer.Point point = new PDollarGestureRecognizer.Point(inputEventDragParams.ScreenPosition.x,
                inputEventDragParams.ScreenPosition.y, _currentStrokeIndex);
            _pointList.Add(point);
            AddPointToLineRenderer(inputEventDragParams.ScreenPosition);
        }
        _touch.State = TouchState.Drag;
    }

    private void OnTouchBEnd(InputEventParams inputEventParams) 
    {
        if (_touch.HasMoved){
            if (!EventSystem.current.IsPointerOverGameObject() && _isTouchBActive){
                _touch.ScreenEndPosition = inputEventParams.ScreenPosition;
                PDollarGestureRecognizer.Point point = new PDollarGestureRecognizer.Point(inputEventParams.ScreenPosition.x,
                    inputEventParams.ScreenPosition.y, _currentStrokeIndex);
                _pointList.Add(point);
                AddPointToLineRenderer(inputEventParams.ScreenPosition);
                _currentStrokeIndex++;
            }  
        }
    }

    #endregion

    public void ConfirmGesture(){
        string match;
        PDollarGestureRecognizer.Gesture gesture = new PDollarGestureRecognizer.Gesture(_pointList.ToArray(), "");
        match = QPointCloudRecognizer.Classify(gesture, _templateList.ToArray());
        ResetGesture();

        if (match != ""){
            EventManager.Instance.OnMultiGesture?.Invoke(match);
        }
    }

    public void ResetGesture(){
        _currentStrokeIndex = 0;
        _pointList.Clear();
        ResetLineRenderer();
    }

    // public void RecordGesture() 
    // {   
    //     string name = _nameInput.text;
    //     _recognizerDollar.SavePattern(name, _pointList);
    //     Debug.Log("Gesture -  " + name + " has been recorded.");
    // }

    // public void WriteGesture() 
    // {
    //     string name = _nameInput.text;
    //     DollarRecognizer.Unistroke unistroke = _recognizerDollar.SavePattern(name, _pointList);
    //     string gestureName = _nameInput.text;
    //     string fileName = string.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, gestureName, DateTime.Now.ToFileTime());
    //     GestureIO.WriteGesture(unistroke, gestureName, fileName);
    //     Debug.Log("Gesture -  " + name + " has been saved.");
    // }

    private void ReadGesture(){
        //Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("Gestures/");
		foreach (TextAsset gestureXml in gesturesXml)
			_recognizerDollar.AddToLibrary(GestureIO.ReadGestureFromXML(gestureXml.text));
            //Add loaded gestures to library.
    }

    #region Line Renderer

    private void AddPointToLineRenderer(Vector2 position){   
        _lineRenderer.positionCount = _pointCount + 1;
        _lineRenderer.SetPosition(_pointCount, new Vector3(position.x, position.y, 0));
        _pointCount++;
    }

    private void ResetLineRenderer(){
        _lineRenderer.positionCount = 0;
    }

    #endregion
}
