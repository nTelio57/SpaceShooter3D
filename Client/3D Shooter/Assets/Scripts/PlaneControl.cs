using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public MouseLook cameraControl;
    public float startingSpeedThreshold;
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
    //[HideInInspector]
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
        if (GameManager.currentGameMode == GameMode.Multiplayer)
            return;

        velocity = Vector3.zero;
        torque = Vector3.zero;

        //----------------FORWARD AND BACKWARDS
        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward * brakeForce;
            //Brake only
            /*if (Mathf.Abs(Vector3.SqrMagnitude(rb.velocity)) > 0)
                rb.AddForce(-rb.velocity.normalized * brakeForce * Time.fixedDeltaTime);*/
        }
        //----------------GO TO LEFT AND RIGHT
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right * sidewaysSpeed;
            //torque -= transform.up * torqueSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.right * sidewaysSpeed;
            //torque += transform.up * torqueSpeed;
        }
        //----------------ASCEND AND DESCENT
        if (Input.GetKey(KeyCode.LeftControl))
        {
            velocity -= transform.up * verticalSpeed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            velocity += transform.up * verticalSpeed;
        }
        //----------------MATCH ROTATION TO CAMERA


        //----------------FIX ROTATION AROUND Z AXIS
        //Needss code here

        velocity = Vector3.ClampMagnitude(velocity, forwardSpeed);

        if(Vector3.SqrMagnitude(rb.velocity)/100 <= startingSpeedThreshold)
            rb.AddForce(velocity * currentBoostMultiplier * rb.mass * Time.fixedDeltaTime / 1.25f, ForceMode.Impulse);
        else
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
        if (Input.GetKeyDown(boostActivationKey))
        {
            isButtonReclickNeeded = false;

        }
        if (Input.GetKey(boostActivationKey) && !isButtonReclickNeeded)
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
