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

    public int LearnLetterIndex {get{return _learnLetterIndex;} set{_learnLetterIndex = value;}}

    public void LoadScene(int sceneIndex){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
 
}
