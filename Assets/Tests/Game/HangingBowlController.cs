using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingBowlController : MonoBehaviour
{
    public void animFinished()
    {
        StartCoroutine(waitASec());
    }

    IEnumerator waitASec()
    {
        yield return new WaitForSeconds(1.5f);
        GameController.Instance.bowlAnimFinished = true;
        // Destroy(gameObject);
    }
}
