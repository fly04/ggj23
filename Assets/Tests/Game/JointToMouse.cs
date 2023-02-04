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
        // transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        joint.connectedAnchor = mousePosition;
        // rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Input.GetAxis("Mouse X") * Time.deltaTime * 10);
    }
}