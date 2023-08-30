using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    private Rigidbody rb;

    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelRearLeft;
    public bool wheelRearRight;


    [Header("Suspension Specs")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLenth;
    private float maxLenth;
    private float lastLenth;
    private float springVelocity;
    private float springLenth;
    private float springForce;
    private float damperForce;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocityLocalState;
    private float Fx;
    private float Fy;

    [Header("Wheel")]
    public float wheelRadius;
    public float steerAngle;
    public float steerTime;

    public float rayLength = 1.0f;

    private float wheelAngle;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLenth = restLength - springTravel;
        maxLenth = restLength + springTravel;
    }

    private void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);


        //Debug ray for suspension
            Debug.DrawRay(transform.position, -transform.up * (springLenth + wheelRadius), Color.green);
        //End debug

        // Debug ray for steering
            // Calculate the direction of the steering ray based on the wheel angle
            Vector3 steeringDirection = Quaternion.Euler(0, wheelAngle, 0) * transform.forward;

            // Draw the steering ray
            if (wheelFrontLeft || wheelRearLeft)
            {
                Debug.DrawRay(transform.position, steeringDirection * rayLength, Color.blue);
            } else if (wheelFrontRight || wheelRearRight)
            {
                Debug.DrawRay(transform.position, -steeringDirection * rayLength, Color.blue);
            }
        //End debug
    }


    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenth + wheelRadius))
        {
            lastLenth = springLenth;
            springLenth = hit.distance - wheelRadius;
            springLenth = Mathf.Clamp(springLenth, minLenth, maxLenth);

            springVelocity = (lastLenth - springLenth) / Time.fixedDeltaTime;

            springForce = springStiffness * (restLength - springLenth);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;


            wheelVelocityLocalState = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)); //get the local space
            Fx = Input.GetAxis("Vertical") * springForce;
            Fy = wheelVelocityLocalState.x * springForce;

            if (Input.GetKey(KeyCode.Space))
            {
                if (rb.drag <= 2.0f)
                    rb.drag += 0.01f;
            } else
            {
                rb.drag = 0f;
            }
  
            rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);
            //rb.AddForceAtPosition(suspensionForce, hit.point);
        }
    }
}
