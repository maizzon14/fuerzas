using UnityEngine;
using UnityEngine.InputSystem;

public class RocketManager : MonoBehaviour
{
    public float force = 300000f;

    PlayerInput input;

    public Rigidbody[] rockets;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (input.actions["Launch"].IsPressed()) // si mantienes presionado despegan, el de 50000 kg ni se mueve debido a que la gravedad de unity es mayor a la aceleracion que tiene
        {                                        //aunque puedes quitar el useGravity de sus rigidbody y así despega
            foreach (Rigidbody rb in rockets)
            {
                rb.AddForce(Vector3.up * force);
            }
        }
    }
}
