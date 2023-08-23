using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] WheelCollider top;
    [SerializeField] WheelCollider bottom;

    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [SerializeField] Transform topTransfrom;
    [SerializeField] Transform bottomTransfrom;


    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;


    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate()
    {

        // Get forward/reverse acceleration from the vertical axis
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        // Debug.DrawRay to visualize the acceleration force
        

        // Giving currentBreakingForce value when from the space
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else currentBreakForce = 0f;

        // Apply acceleration to front wheels
        backLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;

        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque= currentBreakForce;
        backRight.brakeTorque = currentBreakForce;


        // Apply acceleration to stearing wheels 
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");

        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        top.steerAngle = currentTurnAngle;

        // Update wheel meshes
        UpdateWheel(top, topTransfrom);
        UpdateWheel(bottom, bottomTransfrom);
    }

    void UpdateWheel(WheelCollider collider, Transform transform)
    {
        //Get wheel collider state
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);

        //Set wheel transfrom data
        transform.position = pos;
        transform.rotation = rot;
    }
}
