using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : MonoBehaviour
{

    [SerializeField] private GameObject draggableCarrot;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float minMoveSpeed;
    Vector2 MousePosition;
    Vector2 rightLimitPosition;
    Vector2 leftLimitPosition;
    [SerializeField] Vector2 maxLeftPosition;
    [SerializeField] Vector2 maxRightPosition;
    float floorPosition;
    string direction;

    public bool isMouseDown = false;
    public bool isMouseIn = false;
    private bool startedRunning = false;

    private int runDirection;

    private bool stop = false;
    private Vector2 destination;


    [SerializeField] float dir;



    // Start is called before the first frame update
    void Start()
    {
        //random float between 2 values
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        floorPosition = transform.position.y;
        direction = "right";
        maxLeftPosition.x = 6.8f;
        maxLeftPosition.y = floorPosition;
        maxRightPosition.x = 19f;
        maxRightPosition.y = floorPosition;
        runDirection = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameController.Instance.isMixerScene)
        {
            if (!startedRunning)
            {
                StartCoroutine(changeDirection());
                startedRunning = true;
            }

            if (transform.position.x >= MousePosition.x - 1 && transform.position.x <= MousePosition.x && MousePosition.y - transform.position.y <= 1)
            {
                destination = maxLeftPosition;
                stop = true;
            }
            else if (transform.position.x <= MousePosition.x + 1 && transform.position.x >= MousePosition.x && MousePosition.y - transform.position.y <= 1)
            {
                destination = maxRightPosition;
                stop = true;
            }

            if (transform.position.x >= maxRightPosition.x)
            {
                destination = maxLeftPosition;
                stop = true;
            }
            else if (transform.position.x <= maxLeftPosition.x)
            {
                destination = maxRightPosition;
                stop = true;
            }
            else if (!stop)
            {

                if (runDirection == 1)
                {
                    destination = maxLeftPosition;
                }
                if (runDirection == 0)
                {
                    destination = maxRightPosition;
                }

            }

            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }
        else
        {
            MousePosition.y = floorPosition;

            rightLimitPosition = MousePosition;
            rightLimitPosition.x += 2;
            leftLimitPosition = MousePosition;
            leftLimitPosition.x -= 2;


            if (transform.position.x <= MousePosition.x - 2 || transform.position.x <= MousePosition.x - 1.5)
            {
                direction = "right";
            }
            if (transform.position.x >= MousePosition.x + 2 || transform.position.x >= MousePosition.x + 1.5)
            {
                direction = "left";
            }

            if (transform.position.x >= MousePosition.x - 2 && transform.position.x <= MousePosition.x + 2)
            {
                if (direction == "left")
                {
                    transform.position = Vector2.MoveTowards(transform.position, leftLimitPosition, moveSpeed * Time.deltaTime);
                }
                if (direction == "right")
                {
                    transform.position = Vector2.MoveTowards(transform.position, rightLimitPosition, moveSpeed * Time.deltaTime);
                }

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, MousePosition, moveSpeed * Time.deltaTime);
            }
        }

        //Anim stuff
        if (transform.position.x < maxLeftPosition.x)
        {
            if (direction == "right")
            {
                transform.localScale = new Vector3(-2f, 2f, 2f);
            }
            if (direction == "left")
            {
                transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }
        else
        {
            Vector3 position = transform.position;
            dir = destination.x - position.x;

            if (dir > 0)
            {
                transform.localScale = new Vector3(-2f, 2f, 2f);
            }
            else
            {
                transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }


        if (GameController.Instance.hasDroppedCarrot && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Flee"))
        {
            // Debug.Log("Fleeing");
            GetComponent<Animator>().CrossFade("Flee", 0.0f);
        }

        //Cursor stuff
        if (CursorController.Instance.canGrab)
        {
            if (isMouseIn && !isMouseDown) CursorController.Instance.SetHandOpen();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;

        if (!CursorController.Instance.canGrab) return;
        CursorController.Instance.canGrab = false;
        Instantiate(draggableCarrot, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnMouseUp()
    {
        isMouseDown = false;
    }

    void OnMouseEnter()
    {
        isMouseIn = true;
    }

    void OnMouseExit()
    {
        isMouseIn = false;
    }

    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(Random.Range(2, 3));

        stop = false;
        runDirection = Random.Range(0, 2);

        //have to add condition to stop the loop
        StartCoroutine(changeDirection());
    }
}

