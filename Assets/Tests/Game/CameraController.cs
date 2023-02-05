using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void moveToNextScreen()
    {
        StartCoroutine(LerpFromTo(transform.position, new Vector3(transform.position.x + 12.9f, transform.position.y, transform.position.z), 1f));
    }

    public void moveToFirstScreen()
    {
        StartCoroutine(LerpFromTo(transform.position, new Vector3(transform.position.x, transform.position.y - 7.55f, transform.position.z), 1f));
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, pos2, t / duration);
            yield return 0;
        }
        transform.position = pos2;
    }
}
