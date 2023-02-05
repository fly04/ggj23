using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private CameraController cameraController;

    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Begin");
    }

    private void OnMouseDown()
    {
        Debug.Log("Button pressed!");
        GetComponent<Animator>().CrossFade("ButtonDown", 0.0f);
        StartCoroutine(waitForButton());
    }

    IEnumerator waitForButton()
    {
        yield return new WaitForSeconds(.3f);
        GetComponent<Animator>().CrossFade("ButtonUp", 0.0f);
        yield return new WaitForSeconds(.4f);
        Debug.Log("Game starts...");
        // StartCoroutine(startTheGame());
        cameraController.moveToFirstScreen();
        yield return new WaitForSeconds(2);
        Debug.Log("has started");
        hasStarted = true;
    }
}
