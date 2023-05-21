using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _score = 0;
    public int missedLetters = 0;
    [SerializeField] GameObject _letterObj;
    [SerializeField] float _spawnInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLetters());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _spawnInterval -= 0.00005f;    
    }

    IEnumerator SpawnLetters()
    {
        while (true) 
        {
            Instantiate(_letterObj);
            yield return new WaitForSeconds(_spawnInterval);
        }
        
    }

    public void GameOver()
    {
        if (missedLetters >= 5)
        {
            StopCoroutine("SpawnLetters");
            //Add game over ui logic here
        }
    }

    public void AddScore()
    {
        _score++;
    }
}
