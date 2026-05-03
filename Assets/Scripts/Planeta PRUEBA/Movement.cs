using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    public float speed = 15f;
    public float jumpHeight = 1.2f;

    public bool isGrounded = false;

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
    }

    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    public void Jump()
    {
        if(isGrounded)
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }
}
