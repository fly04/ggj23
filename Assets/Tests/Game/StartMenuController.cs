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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Started!");
        StartCoroutine(startTheGame());
        cameraController.moveToFirstScreen();
        
    }

     IEnumerator startTheGame()
    {
        yield return new WaitForSeconds(4);

        hasStarted = true;
    }

}
