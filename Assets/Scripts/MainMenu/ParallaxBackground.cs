using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float _parallax;
    [SerializeField] private float _lenght;
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private bool _paused = false;
    private Vector2 _startPosition;
    private RectTransform _rectTransform;

    public bool Paused {set{_paused = value;}}

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = (Vector2)_rectTransform.localPosition;
    }

    private void Scroll(){
        float delta = _scrollSpeed * _parallax * Time.unscaledDeltaTime;
        _rectTransform.localPosition += new Vector3(delta, 0f, 0f);
    }

    private void CheckReset(){
        if((Mathf.Abs(_rectTransform.localPosition.x) - _lenght) > 0){
            _rectTransform.localPosition = new Vector3(0f, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
        }
    }

    void LateUpdate()
    {
        if (_paused) return;
        Scroll();
        CheckReset();
    }
}
