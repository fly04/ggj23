using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }

    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D handOpenTexture;
    [SerializeField] private Texture2D handClosedTexture;
    [SerializeField] public bool canGrab = true;

    bool mustHandleCarrots = false;
    GameObject[] carrots;
    bool mouseIn = false;

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

    private void Update()
    {

        if (mustHandleCarrots)
        {
            //Find gameObject by tag
            carrots = GameObject.FindGameObjectsWithTag("Carrot");

            bool isDefault = true;
            //for each carrot
            foreach (GameObject carrot in carrots)
            {
                if (carrot.GetComponent<CarrotScript>().isMouseIn || carrot.GetComponent<CarrotScript>().isMouseDown)
                {
                    isDefault = false;
                }
            }

            if (isDefault)
            {
                SetDefault();
            }
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
