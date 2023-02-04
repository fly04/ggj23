using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }


    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D handOpenTexture;
    [SerializeField] private Texture2D handClosedTexture;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetHandOpen()
    {
        Cursor.SetCursor(handOpenTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetHandClosed()
    {
        Cursor.SetCursor(handClosedTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetDefault()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
