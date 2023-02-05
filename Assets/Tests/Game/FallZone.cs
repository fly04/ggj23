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

            //disable gameObject
            collision.gameObject.SetActive(false);


            if (parent.gameObject.tag == "HangingCarrot")
            {
                //set order in layer
                parent.GetComponent<SpriteRenderer>().sortingOrder = 2;
                parent.GetComponent<HangingCarrotController>().hasBeenDropped = true;
                GameController.Instance.hasDroppedCarrot = true;
                GameController.Instance.droppedCount++;
            }

            if (parent.gameObject.tag == "Bowl")
            {
                StartCoroutine(waitForBowlFall(parent));
            }
        }
    }

    IEnumerator waitForBowlFall(GameObject parent)
    {
        Debug.Log("Bowl fall");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Bowl anim");
        parent.GetComponent<Animator>().CrossFade("BowlClosed", 0.0f);
    }
}
