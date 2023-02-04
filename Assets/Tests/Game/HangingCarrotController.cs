using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingCarrotController : MonoBehaviour
{
    public bool hasBeenDropped = false;

    private void Update()
    {
        if (hasBeenDropped)
        {
            GetComponent<Animator>().CrossFade("StillCarrot", 0.0f);
        }
    }
}
