using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<Axis> Axles; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float maxBreakTorque;
    public Transform centerOfMass;
    public Rigidbody rigid;

    private void Start()
    {
        rigid.centerOfMass = centerOfMass.localPosition;   
    }

    public void FixedUpdate()
    {
        float motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
        float steerAngle = maxSteeringAngle * Input.GetAxis("Horizontal");

        float brakeTorque = 0;
        Vector3 localVelocity = transform.InverseTransformDirection(rigid.velocity);
        if(localVelocity.z > 0 && Input.GetAxis("Vertical") < -0.05f)
        {
            brakeTorque = maxBreakTorque;
        }

        foreach (Axis axis in Axles)
        {
            if (axis.steering)
            {
                axis.leftWheel.steerAngle = steerAngle;
                axis.rightWheel.steerAngle = steerAngle;
            }
            if (axis.motor)
            {
                axis.leftWheel.motorTorque = motorTorque;
                axis.rightWheel.motorTorque = motorTorque;
            }
            axis.leftWheel.brakeTorque = brakeTorque;
            axis.rightWheel.brakeTorque = brakeTorque;
        }
    }
}

[System.Serializable]
public class Axis
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

