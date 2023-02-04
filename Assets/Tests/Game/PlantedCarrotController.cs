using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedCarrotController : MonoBehaviour
{
    public bool hasGrown = false;
    public bool hasBeenDugUp = false;
    public bool isMouseDown = false;

    public void carrotAnimationFinished()
    {
        hasGrown = true;
    }

    private void Update()
    {
        if (GetComponent<PlayOnDrag>().frameIndex == GetComponent<PlayOnDrag>().frames.Length - 1)
        {
            hasBeenDugUp = true;
        }

        if (!isMouseDown && GetComponent<PlayOnDrag>().frameIndex == 0) GetComponent<Animator>().enabled = true;
    }

    void OnMouseDown()
    {
        isMouseDown = true;
        GetComponent<Animator>().enabled = false;
    }

    void OnMouseUp()
    {
        isMouseDown = false;
    }
}
