using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{
    

    public float maxThrust = 10f;
    public GameObject CargoClaw;
    public float rolloverTimeout = 0.5f;
    public float pickupRange = 0.6f;
    public AnimationCurve ThrustCurve;

    public ParticleSystem leftEmitter, rightEmitter;
    public JetAudio leftJetAudio, rightJetAudio;

    private ParticleSystem.EmissionModule leftEm, rightEm;

    private bool leftThrust;
    private bool rightThrust;
    private float thrustDuration;
    private Stackable cargo;
    private bool isDropping;

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

        leftEm = leftEmitter.emission;
        rightEm = rightEmitter.emission;

        leftEm.enabled = false;
        rightEm.enabled = false;
        isDropping = false;
    }


    // Update is called once per frame
    void Update()
    {
        HandleVelocityInput();
        HandleCargo();  
    }

    private void HandleVelocityInput()
    {
        var leftHold = Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var leftDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        var leftUp = Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);

        var rightHold = Input.GetMouseButton(1) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var rightDown = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        var rightUp = Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);


        //Left Thruster
        leftThrust = leftHold;
        leftEm.enabled = leftThrust;
        if (leftUp)
            leftJetAudio.Stop();
        if (leftDown)
            leftJetAudio.Play();

        //Right Thruster
        rightThrust = rightHold;
        rightEm.enabled = rightThrust;
        if (rightUp)
            leftJetAudio.Stop();
        if (rightDown)
            rightJetAudio.Play();
    }

    private void FixedUpdate()
    {
        rb.AddForce(CustomGravity.GetDirection(transform.position), ForceMode.Force);

        HandleVelocity();
    }

    private void HandleCargo()
    {
        if (isDropping)
            return;

        //If the player is holding a stackable object
        if (cargo != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DropStackable());
            }
        }

        //if the player has no stackable object
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //Set the parameter "grabbing" to true
                anim.SetBool("grabbing", true);

                //If on top of a stackable object, pick up the stackable object
                int layerMask = 1 << LayerMask.NameToLayer("SpaceObject");
                Collider[] objects = Physics.OverlapSphere(CargoClaw.transform.position, 1, layerMask);
                if (objects.Length > 0)
                {
                    foreach(Collider c in objects)
                    {
                        Stackable s = c.gameObject.GetComponent<Stackable>();
                        if (s != null)
                        {
                            PickUpStackable(s);
                            anim.SetBool("grabbing", false);
                            break;
                        }
                        else
                        {
                            Debug.Log($"SpaceObject {c.gameObject.name} is not Stackable");
                        }
                    }
                }
            }
            else
            {
                //Set the parameter "grabbing" to false
                anim.SetBool("grabbing", false);
            }
        }
    }

    private void PickUpStackable(Stackable stackable)
    {
        cargo = stackable;
        //Add the mass of the stackable to the player, disable the rigidbody of the stackable
        rb.mass += stackable.mass;

        //Attach the stackable to the cargo claw
        cargo.CasheRigidbody();
        cargo.transform.parent = CargoClaw.transform;
        cargo.transform.localPosition = Vector3.zero;
    }

    private IEnumerator DropStackable()
    {
        isDropping = true;
        anim.SetBool("grabbing", true);

        float dropTime = 0;
        while (dropTime <= 0.5f)
        {
            dropTime += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("grabbing", true);

        //Detach the stackable from the cargo claw
        cargo.transform.parent = Stackable.stackableParent;
        cargo.RestoreRigidbody();        

        //Remove the mass of the stackable from the player, enable the rigidbody of the stackable
        rb.mass -= cargo.mass;

        cargo = null;
        isDropping = false;
    }

    void HandleVelocity()
    {
        //Both thrusters
        if (leftThrust && rightThrust)
        {
            thrustDuration += Time.deltaTime;
            float thrust = ThrustCurve.Evaluate(thrustDuration) * maxThrust;
            rb.AddForceAtPosition(transform.up * thrust * Time.deltaTime, transform.position);
        }

        //Only one thruster
        else if (leftThrust)
        {
            rb.AddTorque(new Vector3(0, 0, -Time.deltaTime) * maxThrust, ForceMode.Acceleration);
        }
        else if (rightThrust)
        {
            rb.AddTorque(new Vector3(0, 0, Time.deltaTime) * maxThrust, ForceMode.Acceleration);
        }

        //No thrusters
        else
        {
            thrustDuration = 0;
        }
        

    }
}