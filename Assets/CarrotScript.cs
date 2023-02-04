using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : MonoBehaviour
{

    [SerializeField] private GameObject draggableCarrot;

    public float moveSpeed;
    Vector2 MousePosition;
    Vector2 rightLimitPosition;
    Vector2 leftLimitPosition;
    float floorPosition;
    string direction;

    public bool isMouseDown = false;
    public bool isMouseIn = false;



    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(2, 5);
        floorPosition = transform.position.y;
        direction = "right";
    }

    // Update is called once per frame
    void Update()
    {


        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        //Cursor stuff
        if (CursorController.Instance.canGrab)
        {
            if (isMouseIn && !isMouseDown) CursorController.Instance.SetHandOpen();
            if (isMouseIn && isMouseDown) CursorController.Instance.SetHandClosed();
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

