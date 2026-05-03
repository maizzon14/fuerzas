using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    public GameObject planet;

    public float speed = 15f;
    public float jumpHeight = 1.2f;

    public float gravity = -100f;
    float distanceToGround;
    Vector3 groundNormal;
    public bool isGrounded = false;
    public float distanciaSueloRAY;

    Rigidbody rb;
    PlayerInput input;
    Vector3 move;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        move = new Vector3(input.actions["Move"].ReadValue<Vector2>().x, 0, input.actions["Move"].ReadValue<Vector2>().y);

        Vector3 gravityDirection = (transform.position - planet.transform.position).normalized;

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -gravityDirection, out hit, 100f))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            if (distanceToGround < distanciaSueloRAY)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        
        rb.AddForce(gravityDirection * gravity);

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontal = transform.TransformDirection(move) * speed;

        rb.linearVelocity = horizontal + Vector3.Project(velocity, transform.up);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            float jumpForce = Mathf.Sqrt(2 * -gravity * jumpHeight);
            rb.linearVelocity += transform.up * jumpForce;
        }
    }
}
