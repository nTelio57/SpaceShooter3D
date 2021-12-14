using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public PlaneControl planeControl;
    public Rigidbody rb;
    public float mouseSensitivity = 100f;
    public float maxTurnSidewaysSpeed = 0.75f;
    public float turnSpeed;

    public Transform playerBody;

    float xRotation = 0f, yRotation = 0f;
    float tempMouseSensitivity = 100;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        tempMouseSensitivity = mouseSensitivity / planeControl.currentBoostMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sukimasis i kaire ir desine
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -60f + playerBody.rotation.eulerAngles.y, 60f + playerBody.rotation.eulerAngles.y);

        //Sukimasis i virsu apacia
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void RotateToMatchPersonsView()
    {
        if (Vector3.SqrMagnitude(rb.velocity) > 10)
        {
            Vector3 v = Vector3.RotateTowards(playerBody.transform.forward, transform.forward, turnSpeed * Time.fixedDeltaTime, 0);
            v.y = Mathf.Clamp(v.y, -0.9f, 0.9f);
            playerBody.rotation = Quaternion.LookRotation(v);
        }
    }
}
