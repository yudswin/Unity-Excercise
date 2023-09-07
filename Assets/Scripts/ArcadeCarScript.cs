using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeCarScript : MonoBehaviour
{
    [Header("Car Fuel")]
    static public float fuelRemaining; // Current fuel level
    public float initialFuelLevel = 100.0f; // Initial fuel level
    public float fuelConsumptionRate = 5.0f; // Rate at which fuel is consumed per second
    public float fuelAddValue = 25.0f;
    public float capacityAddValue = 25.0f;
    public float moveInput;
    private bool _isMoving;

    [Header("Car Damaged")]
    public float damageStatus = 100.0f;
    public float initialStatus = 100.0f;
    public float damageValue = 10.0f;

    [Header("UI")]
    public Text fuelText;
    public Text damageText;

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        _isMoving = moveInput != 0 ? true : false;

        if (_isMoving && fuelRemaining > 0) ConsumingFuel();
        DamageingCar();
    }

    void Start()
    {
        fuelRemaining = initialFuelLevel;
        fuelText.text = fuelRemaining.ToString();
    }


    // Update the UI 
    void ConsumingFuel()
    {
        float fuelConsumed = fuelConsumptionRate * Time.deltaTime;
        int fuel;
        if (fuelConsumed > 0)
        {
            fuelRemaining -= fuelConsumed;
            fuel = (int)fuelRemaining;
            fuelText.text = fuel.ToString();
        }
    }

    void DamageingCar()
    {
        int damage;
        damage = (int)damageStatus;
        damageText.text = damage.ToString();
    }
    // End Update the UI


    [ContextMenu("Add Fuel")]
    public void AddFuel()
    {
        if (fuelRemaining < initialFuelLevel)
        {
            if (fuelRemaining + fuelAddValue >= initialFuelLevel)
            {
                fuelRemaining += (initialFuelLevel - fuelRemaining);
            }
            else if (fuelRemaining + fuelAddValue < initialFuelLevel)
            {
                fuelRemaining += fuelAddValue;
            }
        }
    }

    // Hit object - barrier
    [ContextMenu("Damage the Car")]
    public void DamageByValue()
    {
        damageStatus -= damageValue;
    }

    // Hit wall
    public void DamageBySpeed()
    {
        if (moveInput >= 0.8f || moveInput <= -0.8f)
        {
            damageStatus -= (10.0f * Mathf.Abs(moveInput));   
        }
    }

    [ContextMenu("Add Capacity")]
    public void AddCapacity()
    {
        initialFuelLevel += capacityAddValue;
    }


    [ContextMenu("Repair the Car")]
    public void RepairCarByValue()
    {
        if (damageStatus < initialStatus)
        {
            if (damageStatus + capacityAddValue >= initialStatus)
            {
                damageStatus += (initialStatus - damageStatus);
            }
            else if (damageStatus + capacityAddValue < initialStatus)
            {
                damageStatus += capacityAddValue;
            }
        }
    }

    public void RepairCarFull()
    {
        damageStatus = initialStatus;
    }
}
