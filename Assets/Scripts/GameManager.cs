using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture, _cursorClickTexture;

    private void Start()
    {
        Screen.SetResolution(1080, 1920, FullScreenMode.Windowed, 30);
        Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update(){
        
        if (Input.GetMouseButton(0)){
            Cursor.SetCursor(_cursorClickTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
