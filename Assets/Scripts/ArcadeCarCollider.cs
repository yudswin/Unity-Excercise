using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Hit!");
        }
    }
}
