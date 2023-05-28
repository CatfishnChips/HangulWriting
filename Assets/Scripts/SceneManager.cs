using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
   #region Singleton

    public static SceneManager Instance;

    private void Awake()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _points = _playerData.Points;
        _mascotSprite = _playerData.Mascot;
    }

    #endregion

    private int _learnLetterIndex = 0;
    private Sprite _mascotSprite;
    private int _points = 0;

    public int LearnLetterIndex {get{return _learnLetterIndex;} set{_learnLetterIndex = value;}}
    public Sprite MascotSprite {get{return _mascotSprite;} set{_mascotSprite = value;}}
    public int Points {get{return _points;} set{_points = value;}}
    public List<ShopItem> ShopItemList {get{return _shopItemList;}}

    [SerializeField] private List<ShopItem> _shopItemList;
    [SerializeField] private PlayerData _playerData;

    public void LoadScene(int sceneIndex){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void UpdatePlayerData(){
        _playerData.Mascot = _mascotSprite;
        _playerData.Points = _points;
    }
}
