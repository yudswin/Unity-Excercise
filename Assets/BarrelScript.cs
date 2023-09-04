using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BarrelScript : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the speed as needed
    Vector3 _rotationAxis = Vector3.up; // Set the rotation axis

    public bool isFuel;
    public bool isCapacity;
    public bool isBarrier;

    public ArcadeCarScript carScript;

    private void Start()
    {
        carScript = GameObject.FindGameObjectWithTag("Car").GetComponent<ArcadeCarScript>();
    }

    void FixedUpdate()
    {
        BarrelAnimation();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Car" && isFuel)
        {
            // Add Fuel
            carScript.AddFuel();
            Destroy(gameObject);
        }

        // Add Capacitys

        // Damage the Car
    }

    void BarrelAnimation()
    {
        transform.Rotate(_rotationAxis * rotationSpeed * Time.deltaTime, Space.World);
    }
}
