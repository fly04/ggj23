using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1BackgroundController : MonoBehaviour
{

    public bool isSeedPlanted = false;
    private AudioSource rebounceSound;

    void Start(){
        rebounceSound = GetComponent<AudioSource>();
    }

    public void seedAnimationFinished()
    {
        isSeedPlanted = true;
    }

    public void PlayRebounceSound(){
       rebounceSound.Play();
    }
}
