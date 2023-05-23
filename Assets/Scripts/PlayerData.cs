using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableObjects/Player Data", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _points;
    [SerializeField] private Sprite _mascot;

    public int Points {get{return _points;} set{_points = value;}}
    public Sprite Mascot {get{return _mascot;} set{_mascot = value;}}
}
