using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float _parallax;
    [SerializeField] private float _lenght;
    [SerializeField] private float _scrollSpeed;
    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = (Vector2)transform.position;
    }

    private void Scroll(){
        float delta = _scrollSpeed * Time.deltaTime * _parallax;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    private void CheckReset(){
        if((Mathf.Abs(transform.position.x) - _lenght) > 0){
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        Scroll();
        CheckReset();
    }
}
