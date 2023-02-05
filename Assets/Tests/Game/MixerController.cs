using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{
    public bool isMixDone = false;

    public void mix()
    {
        GetComponent<Animator>().CrossFade("MixerAnim", 0.0f);
    }

    public void mixAnimationFinished()
    {
        isMixDone = true;
    }
}
