using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _learnMenu, _shopMenu;

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
}
