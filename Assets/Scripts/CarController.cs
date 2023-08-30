using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelScript[] wheels;

    [Header("Car Specs")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;

    [Header("Inputs")]
    public float steerInput;
    public float ackermannAngleLeft;
    public float ackermannAngleRight;


    private void FixedUpdate()
    {
        steerInput = Input.GetAxis("Horizontal");

        if (steerInput > 0)
        {
            //turn right
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        } else if (steerInput < 0)
        {
            //turn left
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            
        } else
        {
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }

        foreach (WheelScript wheel in wheels)
        {
            if (wheel.wheelFrontLeft)
                wheel.steerAngle = ackermannAngleLeft;
            if (wheel.wheelFrontRight)
                wheel.steerAngle = ackermannAngleRight;
            
        }
    }
}
