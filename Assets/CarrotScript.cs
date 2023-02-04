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
    Vector2 maxLeftPosition;
    Vector2 maxRightPosition;
    float floorPosition;
    string direction;

    public bool isMouseDown = false;
    public bool isMouseIn = false;



    // Start is called before the first frame update
    void Start()
    {
        //random float between 2 values
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        floorPosition = transform.position.y;
        direction = "right";
        maxLeftPosition.x = 6.5f;
        maxLeftPosition.y = floorPosition;
        maxRightPosition.x = 19;
        maxRightPosition.y = floorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.y = floorPosition;
        if (GameController.Instance.isMixerScene)
        {
            if (transform.position.x < MousePosition.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, maxLeftPosition, moveSpeed * Time.deltaTime);
            }
            else if (transform.position.x > MousePosition.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, maxRightPosition, moveSpeed * Time.deltaTime);
            }
        }
        else
        {

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
        if (direction == "right")
        {
            transform.localScale = new Vector3(-1.4f, 1.4f, 1.4f);
        }
        if (direction == "left")
        {
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
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
}

