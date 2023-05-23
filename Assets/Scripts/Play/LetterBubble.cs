using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBubble : MonoBehaviour
{
    [SerializeField] bool isPopped = false;
    PlayLetterManager _letterManager;
    [SerializeField] float _randomInitPush = 1f;
    [SerializeField] float _shrinkFactor = 0.99998f;
    [SerializeField] TextMeshPro _text;
    [SerializeField] string _letterHangul;
    [SerializeField] string _letterLatin;
    [SerializeField] LetterBubbleScriptable[] _lettersHangulLatin = new LetterBubbleScriptable[36];
    int _random;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _random = Random.Range(0, 36);
        _letterHangul = _lettersHangulLatin[_random].letterHangul;
        _letterLatin = _lettersHangulLatin[_random].letterLatin;
        _text.text = _letterHangul;
        _letterManager = FindObjectOfType<PlayLetterManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody2D.AddForce(new Vector2(Random.Range(-_randomInitPush, _randomInitPush), Random.Range(-_randomInitPush, _randomInitPush)));
    }

    void Update()
    {
        if (transform.localScale.x <= 0f && isPopped == false)
        {
            _letterManager.missedLetters++;
            isPopped = true;
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = transform.localScale * _shrinkFactor;
    }
}
