using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedCarrotController : MonoBehaviour
{
    public bool hasGrown = false;
    public bool hasBeenDugUp = false;

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
    }
}
