using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1f;
    private float _startY;

    private void Start(){
        _startY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, _startY + Mathf.Sin(Time.timeSinceLevelLoad) * _multiplier, transform.position.z);
    }
}
