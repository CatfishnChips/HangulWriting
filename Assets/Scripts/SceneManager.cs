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
    }

    #endregion

    private int _learnLetterIndex = 0;
    private Sprite _mascotSprite;
    private int _points = 1000;

    public int LearnLetterIndex {get{return _learnLetterIndex;} set{_learnLetterIndex = value;}}
    public Sprite MascotSprite {get{return _mascotSprite;} set{_mascotSprite = value;}}
    public int Points {get{return _points;} set{_points = value;}}

    [SerializeField] private Sprite _defaultMascotSprite;

    private void Start(){
        _mascotSprite = _defaultMascotSprite;
    }

    public void LoadScene(int sceneIndex){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
