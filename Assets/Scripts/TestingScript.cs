using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    IEnumerable Delay()
    {
        Debug.Log("Delaying... 0s");
        yield return new WaitForSeconds(2);
        Debug.Log("Delaying... 2s");
    }
}
