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

    public bool mustHandleCarrots = false;
    GameObject[] carrots;

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
            if (canGrab)
            {
                //Find gameObject by tag
                carrots = GameObject.FindGameObjectsWithTag("RunningCarrot");

                bool isDefault = true;
                //for each carrot
                foreach (GameObject carrot in carrots)
                {
                    // Debug.Log("Carrot: " + carrot.name);
                    if (carrot.gameObject.GetComponent<CarrotScript>().isMouseIn || carrot.gameObject.GetComponent<CarrotScript>().isMouseDown)
                    {
                        isDefault = false;
                    }
                }

                if (isDefault)
                {
                    SetDefault();
                }
            }
            else
            {
                SetHandClosed();
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
