using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeCarScript : MonoBehaviour
{
    [Header("Car Information")]
    static public float fuelRemaining; // Current fuel level
    public float initialFuelLevel = 100.0f; // Initial fuel level
    public float fuelConsumptionRate = 5.0f; // Rate at which fuel is consumed per second
    public const float fuelToAdd = 25.0f;

    [Header("UI")]
    public Text fuelText;
    //public Text damagedText;

    public bool _isMoving;

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Vertical");
        _isMoving = moveInput != 0 ? true : false;

        if (_isMoving && fuelRemaining > 0) ConsumingFuel();
    }

    void Start()
    {
        fuelRemaining = initialFuelLevel;
        fuelText.text = fuelRemaining.ToString();
    }

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

    [ContextMenu("Add Fuel")]
    public  void AddFuel()
    {
        if (fuelRemaining < initialFuelLevel)
        {
            if (fuelRemaining + fuelToAdd >= initialFuelLevel)
            {
                fuelRemaining += (initialFuelLevel - fuelRemaining);
            }
            else if (fuelRemaining + fuelToAdd < initialFuelLevel)
            {
                fuelRemaining += fuelToAdd;
            }
        }
    }
}
