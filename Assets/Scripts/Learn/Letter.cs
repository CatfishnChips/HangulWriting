using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Letter", fileName = "New Letter")]
[System.Serializable]
public class Letter : ScriptableObject
{
    public string _name;
    public List<Part> _parts = new List<Part>();
    [TextArea(2,4)] public string _information;
}

[System.Serializable]
public struct Part
{
    public Sprite Sprite;
    public string Gesture;
    public Vector2 StartPos;
    public Vector2 EndPos;
    public float StartRot;
    public float EndRot;
}
