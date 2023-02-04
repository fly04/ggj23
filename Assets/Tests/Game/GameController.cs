using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class GameController : MonoBehaviour
{
    private StateMachine fsm;

    /* SCENE 1 STUFF */
    [SerializeField] private Scene1BackgroundController scene1BackgroundController;
    [SerializeField] private GameObject plantedCarrot;
    [SerializeField] private float hangingCarrotOffset;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new StateMachine();
        fsm.AddState("Scene1Fall", new State(
            onLogic: (state) =>
            {
                if (scene1BackgroundController.isSeedPlanted && !plantedCarrot.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GrowingCarrot"))
                {
                    plantedCarrot.GetComponent<Animator>().CrossFade("GrowingCarrot", 0.0f);
                }
            }

        ));
        fsm.AddState("Scene1DigUp", new State(
            onEnter: (state) =>
            {
                plantedCarrot.GetComponent<Animator>().enabled = false;
                plantedCarrot.GetComponent<PlayOnDrag>().enabled = true;
                plantedCarrot.GetComponent<BoxCollider2D>().enabled = true;
            }
        ));
        fsm.AddState("Scene1Hang", new State(
            onEnter: (state) =>
            {
                plantedCarrot.GetComponent<PlayOnDrag>().enabled = false;
                plantedCarrot.GetComponent<BoxCollider2D>().enabled = false;
                plantedCarrot.GetComponent<Animator>().enabled = true;
                plantedCarrot.GetComponent<Animator>().CrossFade("HangingCarrot", 0.0f);
            },
            onLogic: (state) =>
            {
                //make plantedCarrot follow cursor
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                plantedCarrot.transform.position = new Vector3(mousePos.x, mousePos.y + hangingCarrotOffset, plantedCarrot.transform.position.z);
            }
        ));

        fsm.AddTransition("Scene1Fall", "Scene1DigUp", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasGrown);
        fsm.AddTransition("Scene1DigUp", "Scene1Hang", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasBeenDugUp);

        fsm.Init();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
    }
}
