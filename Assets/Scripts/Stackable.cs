using UnityEngine;

public class Stackable : MonoBehaviour
{
    private static Transform _stackableParent;

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
    private bool isKinematic;
    private bool detectCollisions;



    public void CasheRigidbody()
    {
        //Save a copy of the rigidbody and then destroy the original
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            mass = rb.mass;
            useGravity = rb.useGravity;
            isKinematic = rb.isKinematic;
            detectCollisions = rb.detectCollisions;
            Destroy(rb);
        }
    }

    public void RestoreRigidbody()
    {
        //Add the mass of the stackable to the player, disable the rigidbody of the stackable
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = mass;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        //Freexe Z motion
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        //Freeze X and Y rotation
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    private void Start()
    {
        //Set the stackable parent to the stackable parent in the scene
        transform.parent = stackableParent;

        //Add the stackable to the camera target group
        FindFirstObjectByType<Cinemachine.CinemachineTargetGroup>().AddMember(transform, 1,1);
    }
}