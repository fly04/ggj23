using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    private StateMachine fsm;

    [Header("References")]
    [SerializeField] private CameraController cameraController;

    /* SCENE 1 STUFF */
    [Header("Scene 1 Stuff")]
    [SerializeField] private Scene1BackgroundController scene1BackgroundController;
    [SerializeField] private GameObject plantedCarrot;
    [SerializeField] private float hangingCarrotOffset;
    [SerializeField] private GameObject draggableCarrot;
    [SerializeField] private GameObject runningCarrot;
    [SerializeField] private GameObject runningBaby;
    public bool isMixerScene = false;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        fsm = new StateMachine();
        fsm.AddState("Scene1Fall", new State());
        fsm.AddState("Scene1Grow", new State(
            onEnter: (state) =>
            {
                plantedCarrot.GetComponent<Animator>().CrossFade("GrowingCarrot", 0.0f);
            }

        ));
        fsm.AddState("Scene1DigUp", new State(
            onEnter: (state) =>
            {
                plantedCarrot.GetComponent<BoxCollider2D>().enabled = true;
                plantedCarrot.GetComponent<PlayOnDrag>().enabled = true;
            }
        ));
        fsm.AddState("Scene1Hang", new State(
            onEnter: (state) =>
            {
                Instantiate(draggableCarrot, plantedCarrot.transform.position, Quaternion.identity);
                Destroy(plantedCarrot);
                CursorController.Instance.canGrab = false;

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(runningCarrot, new Vector3(Random.Range(-7, -10), -1.6f, 0), Quaternion.identity);
                }
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(runningBaby, new Vector3(Random.Range(-7, -10), -1.6f, 0), Quaternion.identity);
                }

            }
        ));

        fsm.AddState("Scene1Mixer", new State(
            onEnter: (state) =>
            {
                StartCoroutine(setIsInMixerScene());
                CursorController.Instance.mustHandleCarrots = true;
                cameraController.moveToNextScreen();
            },
            onExit: (state) => isMixerScene = false
        ));

        fsm.AddTransition("Scene1Fall", "Scene1Grow", transition => scene1BackgroundController.isSeedPlanted);
        fsm.AddTransition("Scene1Grow", "Scene1DigUp", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasGrown);
        fsm.AddTransition("Scene1DigUp", "Scene1Hang", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasBeenDugUp);
        fsm.AddTransition("Scene1Hang", "Scene1Mixer", transition => Input.anyKeyDown);

        fsm.Init();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
    }

    IEnumerator setIsInMixerScene()
    {
        yield return new WaitForSeconds(1.0f);
        isMixerScene = true;
    }

}
