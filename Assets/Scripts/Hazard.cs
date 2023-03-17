using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Vector3 initialVelocity;

    private Collider _col;
    private Collider col
    {
        get
        {
            if (_col == null)
            {
                _col = GetComponent<Collider>();
            }
            return _col;
        }
    }

    private Rigidbody _rb;
    private Rigidbody rb
    {
        get
        {
            if(_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }

    public float mass;
    private bool useGravity;

    private void Awake()
    {
        useGravity = true;
        mass = rb.mass;
    }

    public void CasheRigidbody()
    {
        //Save a copy of the rigidbody and then destroy the original
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        if(col != null)
        {
            col.enabled = false;
        }
        useGravity = false;
    }

    public void RestoreRigidbody()
    {
        if(rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        if (col != null)
        {
            col.enabled = true;
        }

        useGravity = true;
    }

    private void Start()
    {
        rb.AddForce(initialVelocity, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if(useGravity)
        {
            rb.AddForce(CustomGravity.GetDirection(transform.position), ForceMode.Force);
        }
    }
}
