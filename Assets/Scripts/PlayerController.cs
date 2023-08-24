using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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


    [Header("Engine")]
    public float acceleration = 500f;
    [Range(0, 2000)]
    public float speed = 200f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;


    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private bool flag = false;
    private DriveMode driveMode;

    private void Start()
    {
        driveMode = DriveMode.MANUAL;
    }

    private void Update()
    {
        // Manual Mode
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (flag)
            {
                driveMode = DriveMode.MANUAL;
                Debug.Log("Manual mode: ON!");
            }
            else
            {
                driveMode = DriveMode.AUTOMATIC;
                Debug.Log("Automatic mode: ON!");
            }
            flag = !flag;
        }
    }

    private void FixedUpdate()
    {
        
        if(driveMode == DriveMode.MANUAL)
        {
            // Get forward/reverse acceleration from the vertical axis
            currentAcceleration = acceleration * Input.GetAxis("Vertical");
            AddAcceleration();

            // Giving currentBreakingForce value when from the space
            if (Input.GetKey(KeyCode.Space))
            {
                currentBreakForce = breakingForce;
            }
            else currentBreakForce = 0f;

            // Apply acceleration to stearing wheels 
            currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
            AddSteering();
        } else if (driveMode == DriveMode.AUTOMATIC)
        {
            currentAcceleration = acceleration * Time.deltaTime * speed;
            Debug.Log("curAcc = " + currentAcceleration);
            AddAcceleration();
        }
        
        // Update wheel meshes
        UpdateMesh();
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


    void AddAcceleration()
    {
        // Apply acceleration to front wheels
        backLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;

        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
    }

    void AddSteering()
    {
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        top.steerAngle = currentTurnAngle;
    }
    void UpdateMesh()
    {
        UpdateWheel(top, topTransfrom);
        UpdateWheel(bottom, bottomTransfrom);
    }
}

public enum DriveMode
{
    MANUAL,
    AUTOMATIC
}
