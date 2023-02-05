using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{
    public bool isMixDone = false;

    public void mixAnimationFinished()
    {
        isMixDone = true;
    }
}
