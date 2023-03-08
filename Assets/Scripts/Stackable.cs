using UnityEngine;

public class Stackable : MonoBehaviour
{
    private static Transform _stackableParent;

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

    //If stackable parent is null find stackable parent in the scene
    static public Transform stackableParent
    {
        get
        {
            if (_stackableParent == null)
            {
                _stackableParent = GameObject.Find("Stackables").transform;
            }
            return _stackableParent;
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
        //Set the stackable parent to the stackable parent in the scene
        transform.parent = stackableParent;
    }

    private void FixedUpdate()
    {
        if(useGravity)
        {
            rb.AddForce(CustomGravity.GetDirection(transform.position), ForceMode.Force);
        }
    }
}