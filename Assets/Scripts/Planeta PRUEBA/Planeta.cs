using UnityEngine;

public class Planeta : MonoBehaviour
{
    public float gravedad;
    Rigidbody rb;
    Rigidbody rbPersona;

    float masaPlaneta;
    float masaPersona;

    public GameObject persona;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbPersona = persona.GetComponent<Rigidbody>();

        masaPlaneta = rb.mass;
        masaPersona = rbPersona.mass;
    }

    private void FixedUpdate()
    {
       float distancia = Vector3.Distance(transform.position, persona.transform.position);
       distancia = Mathf.Max(distancia, 1f);

       float fuerzaG = (gravedad * masaPlaneta) / (distancia * distancia);

       Vector3 direccion = (transform.position - persona.transform.position).normalized;

       rbPersona.AddForce(direccion * fuerzaG);
       rbPersona.transform.up = (rbPersona.position - transform.position).normalized;
    }
}
