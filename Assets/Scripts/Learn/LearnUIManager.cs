using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LearnUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _arrow;
    [SerializeField] private RectTransform _tip;
    [SerializeField] private List<GameObject> _steps;
    [SerializeField] private List<Image> _stepLetters;

    [SerializeField] private TMP_InputField _letterInput; //Temporary

    [SerializeField] private Image _mascotImage;
    [SerializeField] private TextMeshProUGUI _infoText;

    public RectTransform Canvas {get{return _canvas;}}

    public void UpdateImage(Sprite sprite){
        _image.sprite = sprite;
    }

    public void EnableIndicators(){
        _arrow.gameObject.SetActive(true);
        _tip.gameObject.SetActive(true);
    }

    public void DisableIndicators(){
        _arrow.gameObject.SetActive(false);
        _tip.gameObject.SetActive(false);
    }

    public void UpdateIndicators(Vector2 startPos, float startRot, Vector2 endPos, float endRot){
        DisableIndicators();
        _tip.anchoredPosition = startPos;
        _tip.rotation = Quaternion.Euler(0, 0, startRot);
        _arrow.anchoredPosition = endPos;
        _arrow.rotation = Quaternion.Euler(0, 0, endRot);
        EnableIndicators();
    }

    public void SetupSteps(Letter letter){
        int number = letter._parts.Count;

        foreach (GameObject item in _steps)
        {
            item.SetActive(false);
        }

        for(int i = 0; i < number; i++){
            _steps[i].SetActive(true);
            _stepLetters[i].sprite = letter._parts[i].Sprite;
        }
    }

    public void UpdateInfo(string info){
        _infoText.text = info;
    }

    public void UpdateMascot(Sprite sprite){
        _mascotImage.sprite = sprite;
    }

    public void UpdateStep(int index){
       
    }

    public void ReturnToMenu(){
        SceneManager.Instance.LoadScene(0);
    }

    public void ResetLetter(){
        EventManager.Instance.OnResetLetter?.Invoke();
    }

    public void BackLetter(){
        EventManager.Instance.OnBackLetter?.Invoke();
    }
}
