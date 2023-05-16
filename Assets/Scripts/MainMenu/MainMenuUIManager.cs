using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _learnMenu, _shopMenu;
    [SerializeField] private List<Image> _mascotImages;
    [SerializeField] private List<Sprite> _mascotSprites;
    [SerializeField] private TextMeshProUGUI _pointText;

    private void Start(){
        SetupSprites();
        _pointText.text = SceneManager.Instance.Points.ToString();
    }

    private void SetupSprites(){
        foreach(Image image in _mascotImages){
            image.sprite = SceneManager.Instance.MascotSprite;
        }
    }

    public void SwitchToMainMenu(){
        _mainMenu.SetActive(true);
        _learnMenu.SetActive(false);
        _shopMenu.SetActive(false);
    }

    public void SwitchToLearnMenu(){
        _mainMenu.SetActive(false);
        _learnMenu.SetActive(true);
        _shopMenu.SetActive(false);
    }

    public void SwitchToShopMenu(){
        _mainMenu.SetActive(false);
        _learnMenu.SetActive(false);
        _shopMenu.SetActive(true);
    }

    public void StartPlayMode(){
        SceneManager.Instance.LoadScene(1);
    }

    public void StartLearnMode(int index){
        SceneManager.Instance.LearnLetterIndex = index;
        DontDestroyOnLoad(SceneManager.Instance);
        SceneManager.Instance.LoadScene(2);
    }

    public void BuyShopItem(int value){
        int index = Mathf.FloorToInt(value / 100);
        int price = (value % 100);
        if (SceneManager.Instance.Points >= price){
            SceneManager.Instance.Points -= price;
            SceneManager.Instance.MascotSprite = _mascotSprites[index - 1];
            SetupSprites();
            _pointText.text = SceneManager.Instance.Points.ToString();
        }
    }
}
