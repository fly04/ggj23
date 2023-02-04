using UnityEngine;

public class JointToMouse : MonoBehaviour
{
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    private Vector2 mousePosition;

    private void Start()
    {
        // rb = GetComponent<rb>();
        joint = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        joint.connectedAnchor = mousePosition;
    }
}