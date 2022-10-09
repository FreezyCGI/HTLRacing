using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    public bool hasCollided;
    public LapManager LapManager;

    // Start is called before the first frame update
    void Start()
    {
        hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            hasCollided = true;
            LapManager.NextLapHitted();
        }
    }
}
