using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{

    public float maxThrust = 10f;
    public GameObject LeftThrust, RightThrust;
    public GameObject CargoClaw;
    public float rolloverTimeout = 0.5f;
    public float pickupRange = 0.6f;
    private float timeAtRest = 0f;

    private Stackable cargo;

    Rigidbody rb;
    Animator anim;


    public void Start()
    {

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleVelocity();
        HandleCargo();
   
        //If the player is upside down and resting, add to the timeAtRest
        //Use the dot product to determine direction
        if (Vector3.Dot(transform.up, Vector3.up) < -0.5 && rb.velocity.magnitude < 0.1)
        {
            timeAtRest += Time.deltaTime;
        }
        //if time at rest is greater than the timeout, reset the player rotation
        if (timeAtRest > rolloverTimeout)
        {
            transform.rotation = Quaternion.identity;
            timeAtRest = 0f;
        }

    }
    private float grabCooldown = 0;
    private void HandleCargo()
    {
        if (grabCooldown > 0)
        {
            grabCooldown -= Time.deltaTime;
        }
        //If the player is holding a stackable object, drop the stackable object
        if (cargo != null && Input.GetKeyDown(KeyCode.Space))
        {
            DropStackable();
            //Block the player from picking up another stackable object for a short time
            grabCooldown = 0.5f;

        }
        //If on top of a stackable object, pick up the stackable object
        if (Input.GetKey(KeyCode.Space) && grabCooldown <= 0)
        {   
           
            //Set the parameter "grabbing" to true
            anim.SetBool("grabbing", true);

            RaycastHit hit;
            if (Physics.Raycast(CargoClaw.transform.position, -CargoClaw.transform.up, out hit, pickupRange, ~LayerMask.NameToLayer("Player")) && cargo == null)
            {
                if (hit.collider.gameObject.GetComponent<Stackable>())
                {
                    PickUpStackable(hit.collider.gameObject.GetComponent<Stackable>());
                }
            }
        }
        else
        {
            //Set the parameter "grabbing" to false
            anim.SetBool("grabbing", false);
        }

    }

    private void PickUpStackable(Stackable stackable)
    {
        cargo = stackable;
        //Add the mass of the stackable to the player, disable the rigidbody of the stackable
        rb.mass += cargo.GetComponent<Rigidbody>().mass;

        //Attach the stackable to the cargo claw
        cargo.gameObject.layer = LayerMask.NameToLayer("Player");
        cargo.CasheRigidbody();
        cargo.transform.parent = transform;
        cargo.transform.localPosition = CargoClaw.transform.localPosition;
    }

    private void DropStackable()
    {
        //Detach the stackable from the cargo claw
        cargo.gameObject.layer = LayerMask.NameToLayer("Default");
        cargo.RestoreRigidbody();
        cargo.transform.parent = Stackable.stackableParent;

        //Remove the mass of the stackable from the player, enable the rigidbody of the stackable
        rb.mass -= cargo.GetComponent<Rigidbody>().mass;

        cargo = null;

    }

    void HandleVelocity()
    {
        //If mouse left down
        if (Input.GetMouseButton(0))
        {
            //Apply uppwards velocity at from the left LeftThrust
            rb.AddForceAtPosition(LeftThrust.transform.up * maxThrust, LeftThrust.transform.position);
        }
        //If mouse right down
        if (Input.GetMouseButton(1))
        {
            //Apply uppwards velocity at from the right RightThrust
            rb.AddForceAtPosition(RightThrust.transform.up * maxThrust, RightThrust.transform.position);
        }



    }
}

