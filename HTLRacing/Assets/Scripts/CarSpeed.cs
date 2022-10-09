using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSpeed : MonoBehaviour
{
    public Rigidbody carRigid;
    public TMP_Text txtSpeed;

    // Update is called once per frame
    void Update()
    {
        txtSpeed.text = "Speed: " + (carRigid.velocity.magnitude * 3.6).ToString("0.00") + " km/h";
    }
}
