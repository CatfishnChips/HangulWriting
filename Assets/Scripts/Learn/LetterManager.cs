using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    [SerializeField] private List<Letter> _letters = new List<Letter>();
    [SerializeField] private float _distanceTreshold = 5f;
    private Letter _currentLetter = null;
    private int _currentPart = 0;
    private LearnUIManager _uiManager;

    private void Awake(){
        _uiManager = FindObjectOfType<LearnUIManager>();
    }

    private void Start(){
        EventManager.Instance.OnGesture += OnGesture;
        EventManager.Instance.OnBackLetter += DeadvancePart;
        EventManager.Instance.OnResetLetter += ResetLetter;

        OnLearnStart();
    }

    private void OnDisable(){
        EventManager.Instance.OnGesture -= OnGesture;
        EventManager.Instance.OnBackLetter -= DeadvancePart;
        EventManager.Instance.OnResetLetter -= ResetLetter;
    }

    private Letter FindLetter(int index){
        return _letters[index];
    }

    private Vector2 ConvertToRect(Vector2 screenPos){
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_uiManager.Canvas, screenPos, null, out  anchoredPosition);
        return anchoredPosition;
    }

    private void AdvancePart(){
        if (_currentLetter._parts.Count - 1 > _currentPart){
            _currentPart ++;
            Part part = _currentLetter._parts[_currentPart];
            _uiManager.UpdateImage(part.Sprite);
            _uiManager.UpdateIndicators(part.StartPos, part.StartRot, part.EndPos, part.EndRot);
        }
        else {
            CompleteLearn();
        }
    }

    private void CompleteLearn(){
        SceneManager.Instance.LoadScene(0);
    }

    private void OnGesture(string name, TouchData touch){
        if (_currentLetter == null) return;

        Part part = _currentLetter._parts[_currentPart];
        if (part.Gesture == name){
            if (Vector2.Distance(part.StartPos, ConvertToRect(touch.ScreenStartPosition)) <= _distanceTreshold) {
                if (Vector2.Distance(part.EndPos, ConvertToRect(touch.ScreenEndPosition)) <= _distanceTreshold) {
                    AdvancePart();
                }
            }
        }
    }

    private void OnLearnStart(){
        int index = SceneManager.Instance.LearnLetterIndex;
        Letter letter = FindLetter(index);
        if (letter != null){
            _currentLetter = letter;
            _currentPart = 0;
            Part part = _currentLetter._parts[_currentPart];
            _uiManager.SetupSteps(_currentLetter);
            _uiManager.UpdateImage(part.Sprite);
            _uiManager.UpdateIndicators(part.StartPos, part.StartRot, part.EndPos, part.EndRot);
            _uiManager.UpdateMascot(SceneManager.Instance.MascotSprite);
            _uiManager.UpdateInfo(_currentLetter._information);
        }
    }

    public void DeadvancePart(){
        if (_currentPart > 0){
            _currentPart --;
            Part part = _currentLetter._parts[_currentPart];
            _uiManager.UpdateImage(part.Sprite);
            _uiManager.UpdateIndicators(part.StartPos, part.StartRot, part.EndPos, part.EndRot);
        }
    }

    public void ResetLetter(){
        OnLearnStart();
    }
    

    // private void ArrangeMovesList(){
    //     Dictionary<string, ComboMoveSpecs> tempDictWriter;
    //     for(int i = 0; i < _ctx.CombosArray.Length; i++){
    //         tempDictWriter = this._comboMovesDict;
    //         for(int j = 0; j < _ctx.CombosArray[i].moves.Length; j++){
    //             ComboMoveSpecs comboMoveSpecs = _ctx.CombosArray[i].moves[j];
    //             string comboAttackName = comboMoveSpecs.theMove.name;
    //             //Debug.Log("It's me!" + comboAttackName);
    //             if(!tempDictWriter.ContainsKey(comboAttackName)){
    //                 tempDictWriter.Add(comboAttackName, comboMoveSpecs);
    //             }
    //             //comboMoveSpecs.DeclareNewPossibleNextMoves();
    //             tempDictWriter = tempDictWriter[comboAttackName].possibleNextMoves;  
    //         }
    //     }
    //     // foreach (KeyValuePair<string, ComboMoveSpecs> item in _comboMovesDict){
    //     //     //Debug.Log("Cem: " + item.Value.theMove.name);

    //     //     //Debug.Log(item.Value.possibleNextMoves.Keys);
    //     //     foreach (KeyValuePair<string, ComboMoveSpecs> kitem in item.Value.possibleNextMoves){
    //     //         Debug.Log(kitem.Value.theMove.name);
    //     //     }
    //     // }
    // }
}
