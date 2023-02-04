using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FallHitbox")
        {
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            parent.GetComponent<HingeJoint2D>().enabled = false;
            parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            parent.GetComponent<Rigidbody2D>().angularVelocity = 0;

            //collisons du container (pour qu'il tombe dedans)
            parent.layer = 8;

            CursorController.Instance.SetDefault();
            CursorController.Instance.canGrab = true;
        }
    }
}
