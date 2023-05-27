using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLetterManager : MonoBehaviour
{
    [SerializeField] int _score = 0;
    [SerializeField] private int _health = 5;
    [SerializeField] GameObject _letterObj;
    [SerializeField] float _spawnInterval;
    [SerializeField] LetterBubbleScriptable[] _lettersHangulLatin = new LetterBubbleScriptable[35];
    [SerializeField] List<LetterBubble> _letterObjList = new List<LetterBubble>();
    private PlayUIManager _uiManager;
    private int _currentHealth;

    private void Awake(){
        _uiManager = FindObjectOfType<PlayUIManager>();
    }
    
    void Start()
    {
        EventManager.Instance.OnMultiGesture += OnGesture;

        _currentHealth = _health;
        StartCoroutine(SpawnLetters());
    }

    private void OnDisable(){
        EventManager.Instance.OnMultiGesture -= OnGesture;
    }

    private void Update(){
        _spawnInterval -= 0.00005f * Time.deltaTime;    
    }

    private void OnGesture(string gesture){
        bool valid = false;

        if (_letterObjList.Count == 0) return;

        for (int i = _letterObjList.Count - 1; i >= 0; i--){
            if (_letterObjList[i].Letter.gestureName == gesture){
                _letterObjList[i].PopBubble();
                valid = true;
            }
        }

        // foreach(LetterBubble bubble in _letterObjList){
        //     if (bubble.Letter.gestureName == gesture){
        //         bubble.PopBubble();
        //         valid = true;
        //     }
        // }
        _uiManager.UpdateReaction(valid);
    }

    IEnumerator SpawnLetters()
    {
        while (true) 
        {
            Instantiate(_letterObj);
            yield return new WaitForSeconds(_spawnInterval);
        }
        
    }

    public LetterBubbleScriptable GetLetter(){
        int random = Random.Range(0, _lettersHangulLatin.Length);
        return _lettersHangulLatin[random];
    }

    public void AddScore()
    {
        _score++;
        SceneManager.Instance.Points++;
        SceneManager.Instance.UpdatePlayerData();
        _uiManager.UpdateScore(_score);
    }

    public void AddToBubbleList(LetterBubble letterBubble){
        _letterObjList.Add(letterBubble);
    }

    public void RemoveFromBubbleList(LetterBubble letterBubble){
        _letterObjList.Remove(letterBubble);
    }

    public void DecreaseHealth(){
        _currentHealth--;

        _uiManager.UpdateHealthBar(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            StopCoroutine("SpawnLetters");
            _uiManager.Finish();
        }
    }
}
