using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision);

        if (collision.gameObject.tag == "Carrot")
        {
            collision.gameObject.GetComponent<HingeJoint2D>().enabled = false;
        }
    }
}
