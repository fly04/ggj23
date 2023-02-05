using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBowlController : MonoBehaviour
{
    public GameObject draggableBowl;
    private bool isMouseDown = false;
    private bool isMouseIn = false;

    private void Update()
    {
        if (CursorController.Instance.canGrab)
        {
            if (isMouseIn && !isMouseDown) CursorController.Instance.SetHandOpen();
            if (!isMouseIn && !isMouseDown) CursorController.Instance.SetDefault();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        CursorController.Instance.SetHandClosed();
        Instantiate(draggableBowl, new Vector3(15.973f, -1.254f, 0), Quaternion.identity);
        GameController.Instance.setSmasherScene();
        Destroy(gameObject);
    }

    void OnMouseUp()
    {
        isMouseDown = false;
    }

    void OnMouseEnter()
    {
        isMouseIn = true;
    }

    void OnMouseExit()
    {
        isMouseIn = false;
    }
}
