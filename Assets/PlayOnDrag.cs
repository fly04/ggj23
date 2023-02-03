using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

enum direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
};

public class PlayOnDrag : MonoBehaviour
{
    [Header("Sprite stuff")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] frames;

    [Header("Drag stuff")]
    [SerializeField] private float distanceThreshold = 10.0f;

    [SerializeField] private direction dirToDrag = direction.UP;

    [Header("Collider stuff")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float minXOffset;
    [SerializeField] private float minYOffset;
    [SerializeField] private float minXSize;
    [SerializeField] private float minYSize;

    [SerializeField] private float maxXOffset;
    [SerializeField] private float maxYOffset;
    [SerializeField] private float maxXSize;
    [SerializeField] private float maxYSize;

    [Header("Cursor stuff")]
    [SerializeField] private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    [Header("Misc/Debug stuff")]
    private Vector3 startMousePosition;
    private int frameIndex = 0;
    private int lastFrameIndex;
    private float distance;
    private bool isMouseDown = false;
    private bool isMouseOn = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        handleDrag();
        handleCollider();

        if (!isMouseOn && !isMouseDown) Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    void OnMouseDown()
    {
        isMouseDown = true;
    }

    void OnMouseUp()
    {
        isMouseDown = false;
        distance = 0;
    }

    void OnMouseEnter()
    {
        isMouseOn = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        isMouseOn = false;
    }

    void handleDrag()
    {
        if (!isMouseDown) return;
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            lastFrameIndex = frameIndex;
        }

        if (Input.GetMouseButton(0))
        {
            switch (dirToDrag)
            {
                case direction.UP:
                    distance = Input.mousePosition.y - startMousePosition.y;
                    break;
                case direction.DOWN:
                    distance = startMousePosition.y - Input.mousePosition.y;
                    break;
                case direction.RIGHT:
                    distance = Input.mousePosition.x - startMousePosition.x;
                    break;
                case direction.LEFT:
                    distance = startMousePosition.x - Input.mousePosition.x;
                    break;
            }

            frameIndex = (int)(distance / distanceThreshold + lastFrameIndex);

            if (frameIndex >= frames.Length)
            {
                frameIndex = frames.Length - 1;
            }
            else if (frameIndex < 0)
            {
                frameIndex = 0;
            }

            spriteRenderer.sprite = frames[frameIndex];
        }
    }

    void handleCollider()
    {
        float xSize = lerp(minXSize, maxXSize, (float)frameIndex / (frames.Length - 1));
        float ySize = lerp(minYSize, maxYSize, (float)frameIndex / (frames.Length - 1));
        float xOffset = lerp(minXOffset, maxXOffset, (float)frameIndex / (frames.Length - 1));
        float yOffset = lerp(minYOffset, maxYOffset, (float)frameIndex / (frames.Length - 1));

        boxCollider.size = new Vector2(xSize, ySize);
        boxCollider.offset = new Vector2(xOffset, yOffset);
    }
}
