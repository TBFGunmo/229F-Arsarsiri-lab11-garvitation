using UnityEngine;
using System.Collections.Generic;

public class Garvitation : MonoBehaviour
{

    Rigidbody rb;
    public static List<Garvitation> otherObj;
    const float G = 0.000667f;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObj == null) 
        {
            otherObj = new List<Garvitation>();
        }
        otherObj.Add(this);

        if (!planet) 
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }

    }

    private void FixedUpdate()
    {
        foreach (Garvitation obj in otherObj) 
        {
            if (obj != this) 
            {
                Attract(obj);
            }
        }
    }

    private void Attract(Garvitation other) 
    {
        Rigidbody otherRigidbody = other.rb;
        Vector3 direction = rb.position - otherRigidbody.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;

        // F = G(m1 * m2) / r^2

        float forceMagnitude = G * (rb.mass * otherRigidbody.mass) / Mathf.Pow(distance, 2);
        Vector3 garvitationalForce = forceMagnitude * direction.normalized;
        otherRigidbody.AddForce(garvitationalForce);


    }

}
