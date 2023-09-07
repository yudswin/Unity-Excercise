using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarCollider : MonoBehaviour
{
    private ArcadeCarScript carScript;

    private void Start()
    {
        carScript = GameObject.FindGameObjectWithTag("Car").GetComponent<ArcadeCarScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Hit!");
        }

        // Detect Wall
        //if (collision.gameObject.tag == "Wall")
        //{
        //    Debug.Log("Hit Wall!");
        //    if (wallCheck == 1)
        //    {
        //        carScript.DamageBySpeed();
        //    
        //} 

    }


}
