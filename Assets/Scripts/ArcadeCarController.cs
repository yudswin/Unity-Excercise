using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour
{

    public Rigidbody sphereRB;

    private float moveInput;
    private float turnInput;
    private bool isGrounded;

    public float GRAVITY = 30.0f;
    public float airDrag;
    public float groundDrag;

    public float forwardSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;


    void Start()
    {
        //detach rigidbody from car
        sphereRB.transform.parent = null;
    }


    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        // adjust speed
        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        // set car pos to sphere
        transform.position = sphereRB.transform.position;

        // set car rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);

        // racast ground check
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        // rotate car to be parallel to the ground
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if (isGrounded)
        {
            sphereRB.drag = groundDrag;
        } else
        {
            sphereRB.drag = airDrag;
        }

    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            sphereRB.AddForce(transform.up * (-GRAVITY));
        }
    }
}
