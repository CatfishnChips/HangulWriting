using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/LetterBubble", fileName = "New Letter Bubble")]
[System.Serializable]
public class LetterBubbleScriptable : ScriptableObject
{
    public string letterHangul;
    public string letterLatin;
}
