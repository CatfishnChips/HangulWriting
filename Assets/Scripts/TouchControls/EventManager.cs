using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager Instance;

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

    #region Application Events

    public UnityAction<string> OnLetterLearnStart;
    public UnityAction OnLetterLearnEnd;

    #endregion
    
    #region Gesture Events

    public UnityAction<string, TouchData> OnGesture;
    public UnityAction<string> OnMultiGesture;

    #endregion
}
