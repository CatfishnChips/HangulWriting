using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _learnMenu, _shopMenu;
    [SerializeField] private List<Image> _mascotImages;
    [SerializeField] private List<GameObject> _costTextList;
    [SerializeField] private List<GameObject> _ownedTextList;
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private Sprite _defaultMascot;

    private void Start(){
        SetupSprites();
        UpdateShopItemMenu();
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

    private void UpdateShopItemMenu(){
        for (int i = 0; i < SceneManager.Instance.ShopItemList.Count; i++){
            if (SceneManager.Instance.ShopItemList[i].Owned){
                _costTextList[i].SetActive(false);
                _ownedTextList[i].SetActive(true);
            }
        }
    }

    public void BuyShopItem(int index){
        ShopItem item = SceneManager.Instance.ShopItemList[index];

        if (item.Owned){
            SceneManager.Instance.MascotSprite = item.Sprite;
            SetupSprites();
        }
        else {
            if (SceneManager.Instance.Points >= item.Cost){
                SceneManager.Instance.Points -= item.Cost;
                item.Owned = true;

                SceneManager.Instance.MascotSprite = item.Sprite;
                SetupSprites();
                _pointText.text = SceneManager.Instance.Points.ToString();
                _costTextList[index].SetActive(false);
                _ownedTextList[index].SetActive(true);
            }
        } 
        SceneManager.Instance.UpdatePlayerData();
    }

    public void ResetMascot(){
        SceneManager.Instance.MascotSprite = _defaultMascot;
        SetupSprites();
        SceneManager.Instance.UpdatePlayerData();
    }
}
