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
    public bool isRepairBox;
    public bool isFullRepairBox;

    ArcadeCarScript _carScript;

    public float delayInSeconds = 2.0f;

    private void Start()
    {
        _carScript = GameObject.FindGameObjectWithTag("Car").GetComponent<ArcadeCarScript>();
    }

    void FixedUpdate()
    {
        if (!isBarrier)
        {
            BarrelRotation();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // Add Fuel
        if (collision.gameObject.tag == "Car" && isFuel)
        {
            _carScript.AddFuel();
            Destroy(gameObject);
        }

        // Add Capacity
        if (collision.gameObject.tag == "Car" && isCapacity)
        {
            _carScript.AddCapacity();
            Destroy(gameObject);
        }

        // Repair Car by value
        if (collision.gameObject.tag == "Car" && isRepairBox)
        {
            _carScript.RepairCarByValue();
            Destroy(gameObject);
        }

        // Repair Car full
        if (collision.gameObject.tag == "Car" && isFullRepairBox)
        {
            _carScript.RepairCarFull();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Damage the Car
        if (collision.gameObject.tag == "Car" && isBarrier)
        {
            Debug.Log("hit barrier");
            _carScript.DamageByValue();
            StartCoroutine(DestroyAfterDelay());
        }
    }

    void BarrelRotation()
    {
        transform.Rotate(_rotationAxis * rotationSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(gameObject);
    }
}
