using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private void Awake() {
        Instance = this;
    }

    #region Scene Event

    public UnityAction OnResetLetter;
    public UnityAction OnBackLetter;

    #endregion
    
    #region Gesture Events

    public UnityAction<string, TouchData> OnGesture;
    public UnityAction<string> OnMultiGesture;

    #endregion
}
