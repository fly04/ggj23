using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSoundPlayer : MonoBehaviour
{

    public AudioSource footStepSound;
    // Start is called before the first frame update
    void Start()
    {
        footStepSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
