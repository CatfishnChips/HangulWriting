using UnityEngine;

[ExecuteInEditMode]
public class Debug_PositionInfo : MonoBehaviour
{
    [SerializeField] private Vector2 _position;
    void Update()
    {
        _position = transform.position;
    }
}
