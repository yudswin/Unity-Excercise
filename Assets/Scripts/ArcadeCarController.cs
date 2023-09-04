using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArcadeCarController : MonoBehaviour
{
    private float moveInput;
    private float turnInput;
    private bool isGrounded;


    public float GRAVITY = 30.0f;
    private float normalDrag;
    public float modifiedDrag;

    public float alignToGroundTime;

    public float forwardSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;
    public Rigidbody sphereRB;
    public Rigidbody carRB;

    


    void Start()
    {
        //detach rigidbody from car
        sphereRB.transform.parent = null;
        carRB.transform.parent = null;
        normalDrag = sphereRB.drag;
    }


    void Update()
    {
        // get Input
        if (ArcadeCarScript.fuelRemaining > 0)
        {
            moveInput = Input.GetAxisRaw("Vertical");
            turnInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            moveInput = 0;
            turnInput = 0;
        }


        // calculate car rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        
        if (isGrounded) 
            transform.Rotate(0, newRotation, 0, Space.World);

        // set car position to sphere
        transform.position = sphereRB.transform.position;

        // racast ground check
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        // rotate car to be parallel to the ground
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        // calculate movement direction
        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        // calculate Drag
        sphereRB.drag = isGrounded ? normalDrag : modifiedDrag;


    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            // add movement
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {   
            // add gravity
            sphereRB.AddForce(transform.up * (-GRAVITY));
        }

        carRB.MoveRotation(transform.rotation);
    }
}
