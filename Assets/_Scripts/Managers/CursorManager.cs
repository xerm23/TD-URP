using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script just changes cursor sprite
/// </summary>
public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private void Start()
    {
        Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
    }
}
