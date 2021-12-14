using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public Player p;
    public Rigidbody rb;
    public MouseLook cameraControl;
    public float forwardSpeed;
    public float sidewaysSpeed;
    public float torqueSpeed;
    public float verticalSpeed;
    public float brakeForce;

    [Header("Turbo boost")]
    public bool isBoostTurnedOn = false;
    public KeyCode boostActivationKey;
    public float boostMultiplier;
    public float currentBoostMultiplier = 1;
    public float boostAcceleration = 1.5f;
    public float boostDeceleration = 2.5f;
    public float maxEnergy;
    public float energyCostPerSecond;
    public float energyRestorePerSecond;
    [HideInInspector]
    public float currentEnergy;
    bool isButtonReclickNeeded = false;

    Vector3 velocity;
    Vector3 torque;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentEnergy = maxEnergy * 0.75f;
    }

    private void Update()
    {
        toggle();
        manageEnergy();
        boost();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = Vector3.zero;
        torque = Vector3.zero;
        bool[] inputs = p.getInputs();
        //----------------FORWARD AND BACKWARDS
        if(inputs[0])
        {
            velocity += transform.forward * forwardSpeed;
        }
        if (inputs[1])
        {
            velocity -= transform.forward * brakeForce;
            //Brake only
            /*if (Mathf.Abs(Vector3.SqrMagnitude(rb.velocity)) > 0)
                rb.AddForce(-rb.velocity.normalized * brakeForce * Time.fixedDeltaTime);*/
        }
        //----------------GO TO LEFT AND RIGHT
        if (inputs[2])
        {
            velocity -= transform.right * sidewaysSpeed;
            //torque -= transform.up * torqueSpeed;
        }
        if (inputs[3])
        {
            velocity += transform.right * sidewaysSpeed;
            //torque += transform.up * torqueSpeed;
        }
        //----------------ASCEND AND DESCENT
        if (inputs[4])
        {
            velocity -= transform.up * verticalSpeed;
        }
        if (inputs[5])
        {
            velocity += transform.up * verticalSpeed;
        }
        //----------------MATCH ROTATION TO CAMERA
        if (Vector3.SqrMagnitude(rb.velocity) > 5)
            cameraControl.RotateToMatchPersonsView();


        //----------------FIX ROTATION AROUND Z AXIS
        //Needss code here



        velocity = Vector3.ClampMagnitude(velocity, forwardSpeed);
        rb.AddForce(velocity * currentBoostMultiplier * rb.mass * Time.fixedDeltaTime);

        rb.AddTorque(torque * Time.fixedDeltaTime, ForceMode.Force);

    }

    void manageEnergy()
    {
        if (isBoostTurnedOn)
        {
            currentEnergy -= energyCostPerSecond * Time.deltaTime;
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isBoostTurnedOn = false;
                isButtonReclickNeeded = true;
            }
        }
        else
        {
            if (currentEnergy < maxEnergy)
                currentEnergy += energyRestorePerSecond * Time.deltaTime;
            if (currentEnergy > maxEnergy)
                currentEnergy = maxEnergy;
        }
    }

    void toggle()
    {
        bool[] inputs = p.getInputs();
        if (inputs[6])
        {
            isButtonReclickNeeded = false;

        }
        if (inputs[7] && !isButtonReclickNeeded)
        {
            isBoostTurnedOn = true;
        }
        else
            isBoostTurnedOn = false;
    }

    void boost()
    {
        if (isBoostTurnedOn)
        {
            if (currentBoostMultiplier < boostMultiplier)
                currentBoostMultiplier += boostAcceleration * Time.deltaTime;
            else
                currentBoostMultiplier = boostMultiplier;
        }
        else
        {
            if (currentBoostMultiplier > 1)
                currentBoostMultiplier -= boostDeceleration * Time.deltaTime;
            else
            {
                currentBoostMultiplier = 1;
            }
        }
    }

}
