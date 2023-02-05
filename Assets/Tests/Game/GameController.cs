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
    [SerializeField] private MixerController mixerController;
    [SerializeField] private GameObject clickableBowlPrefab;
    [SerializeField] private Animator backgroundSeed;

    [Header("Starte Menu Stuff")]
    [SerializeField] private GameObject startButton;

    /* SCENE 1 STUFF */
    [Header("Scene 1 Stuff")]
    [SerializeField] private Scene1BackgroundController scene1BackgroundController;
    [SerializeField] private GameObject plantedCarrot;
    [SerializeField] private float hangingCarrotOffset;
    [SerializeField] private GameObject draggableCarrot;
    [SerializeField] private GameObject runningCarrot;
    [SerializeField] private GameObject runningBaby;
    [SerializeField] private GameObject runningFatty;
    public bool isMixerScene = false;
    public bool hasDroppedCarrot = false;
    public int droppedCount = 0;

    bool toFillMixer = false;
    bool toSmasher = false;


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
        fsm.AddState("StartMenu", new State());
        fsm.AddState("Scene1Fall", new State(
            onEnter: (state) =>
            {
                Debug.Log("Scene1Fall");
                backgroundSeed.CrossFade("BackgroundSeed", 0.0f);
            }
        ));
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

                for (int i = 0; i < 2; i++)
                {
                    Instantiate(runningCarrot, new Vector3(Random.Range(-7, -10), -1.6f, 0), Quaternion.identity);
                }
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(runningBaby, new Vector3(Random.Range(-7, -10), -1.6f, 0), Quaternion.identity);
                }
                Instantiate(runningFatty, new Vector3(Random.Range(-7, -10), -1.6f, 0), Quaternion.identity);
                StartCoroutine(goToFillMixer());
            }
        ));

        fsm.AddState("Scene1FillMixer", new State(
            onEnter: (state) =>
            {
                StartCoroutine(setIsInMixerScene());
                CursorController.Instance.mustHandleCarrots = true;
                cameraController.moveToNextScreen();
            },
            onExit: (state) =>
            {
                isMixerScene = false;
                CursorController.Instance.mustHandleCarrots = false;
            }
        ));

        fsm.AddState("Scene1MixerMix", new State(
            onEnter: (state) =>
            {
                Debug.Log("MIXING");
                StartCoroutine(waitForMixing());
            }
        ));

        fsm.AddState("PickUpBowl", new State(
            onEnter: (state) =>
            {
                Debug.Log("PickUpBowl");
                Instantiate(clickableBowlPrefab, new Vector3(15.973f, -1.254f, 0), Quaternion.identity);
                StartCoroutine(goToSmasher());
            },
            onExit: (state) =>
            {
                cameraController.moveToNextScreen();
            }
        ));

        fsm.AddState("Smasher", new State(
            onEnter: (state) =>
            {
                Debug.Log("Smasher enter");
            }
        ));

        fsm.AddTransition("StartMenu", "Scene1Fall", transition => startButton.GetComponent<StartMenuController>().hasStarted);
        fsm.AddTransition("Scene1Fall", "Scene1Grow", transition => scene1BackgroundController.isSeedPlanted);
        fsm.AddTransition("Scene1Grow", "Scene1DigUp", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasGrown);
        fsm.AddTransition("Scene1DigUp", "Scene1Hang", transition => plantedCarrot.GetComponent<PlantedCarrotController>().hasBeenDugUp);
        fsm.AddTransition("Scene1Hang", "Scene1FillMixer", transition => Input.anyKeyDown);
        fsm.AddTransition("Scene1Hang", "Scene1FillMixer", transition => toFillMixer);
        fsm.AddTransition("Scene1FillMixer", "Scene1MixerMix", transition => droppedCount == 6);
        fsm.AddTransition("Scene1MixerMix", "PickUpBowl", transition => mixerController.isMixDone);
        fsm.AddTransition("PickUpBowl", "Smasher", transition => toSmasher);

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

    IEnumerator goToFillMixer()
    {
        yield return new WaitForSeconds(10.0f);
        toFillMixer = true;
    }

    IEnumerator waitForMixing()
    {
        yield return new WaitForSeconds(1.0f);
        mixerController.mix();
    }

    IEnumerator goToSmasher()
    {
        yield return new WaitForSeconds(3.0f);
        toSmasher = true;
    }
}
