using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int lapCounter;
    public Text lapCounterText;

    [ContextMenu("Increase Lap")]
    public void addLap()
    {
        lapCounter += 1;
        lapCounterText.text = lapCounter.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Car")
            addLap();
    }
}
