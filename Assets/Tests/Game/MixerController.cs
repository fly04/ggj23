using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{
    public bool isMixDone = false;
    private AudioSource blenderSound;

    [SerializeField] public GameObject ClickableBowl;

    void Start()
    {
        blenderSound = GetComponent<AudioSource>();
    }

    public void mixAnimationFinished()
    {
        isMixDone = true;
    }

    public void PlayMixerSound(){
       blenderSound.Play();
    }

    public void StopMixerSound(){
       blenderSound.Stop();
    }
}
