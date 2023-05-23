using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1f;
    private RectTransform _rectTransform;

    private void Start(){
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _rectTransform.localPosition = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad) * _multiplier, 0);
    }
}
