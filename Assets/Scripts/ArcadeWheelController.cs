using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeWheelController : MonoBehaviour
{
    public GameObject[] wheels;
    public float rotationSpeed;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        // Wheels Rotation
        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(Time.deltaTime * verticalAxis * rotationSpeed, 0f, 0f, Space.Self);
        }

        // Turning Animation
        if (horizontalAxis > 0)
        {
            // Turning right
            anim.SetBool("goingLeft", false);
            anim.SetBool("goingRight", true);
        } else if (horizontalAxis < 0)
        {
            // Turning left
            anim.SetBool("goingRight", false);
            anim.SetBool("goingLeft", true);
        } else
        {
            // Not turning - Go Straight
            anim.SetBool("goingLeft", false);
            anim.SetBool("goingRight", false);

        }
    }
}
