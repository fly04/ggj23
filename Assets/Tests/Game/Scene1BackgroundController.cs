using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1BackgroundController : MonoBehaviour
{

    public bool isSeedPlanted = false;

    public void seedAnimationFinished()
    {
        isSeedPlanted = true;
    }
}
