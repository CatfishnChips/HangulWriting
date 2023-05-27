using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBubble : MonoBehaviour
{
    [SerializeField] bool isPopped = false;
    PlayLetterManager _letterManager;
    [SerializeField] float _randomInitPush = 1f;
    [SerializeField] float _shrinkFactor = 0.5f;
    [SerializeField] TextMeshPro _text;
    private LetterBubbleScriptable _letter;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Color _defaultColor, _alternateColor;

    public LetterBubbleScriptable Letter {get{return _letter;}}

    private void Awake()
    {
        _letterManager = FindObjectOfType<PlayLetterManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _letter= _letterManager.GetLetter();

        int random = Random.Range(0, 101);
        // 25% chance to be Latin, 75% chance to be Hangul
        if (random > 75){
            _text.text = _letter.letterLatin;
            _text.color = _alternateColor;
        }
        else
        {
            _text.text = _letter.letterHangul;
            _text.color = _defaultColor;
        }

        _letterManager.AddToBubbleList(this);
        _rigidbody2D.AddForce(new Vector2(Random.Range(-_randomInitPush, _randomInitPush), Random.Range(-_randomInitPush, _randomInitPush)));
    }

    void Update()
    {   transform.localScale -= transform.localScale * _shrinkFactor * Time.deltaTime;

        if (transform.localScale.x <= 1f)
        {
            _letterManager.DecreaseHealth();
            _letterManager.RemoveFromBubbleList(this);
            Destroy(gameObject);
        }
    }

    public void PopBubble(){
        _letterManager.AddScore();
        _letterManager.RemoveFromBubbleList(this);
        Destroy(gameObject);
    }
}
