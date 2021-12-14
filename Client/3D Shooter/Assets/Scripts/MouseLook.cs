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
    public float speedNeededForViewSync = 20;
    public float thresholdAngle = 5;

    public Transform playerBody;

    float xRotation = 0f, yRotation = 0f;
    float tempMouseSensitivity = 100;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //tempMouseSensitivity = mouseSensitivity / planeControl.currentBoostMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Sukimasis i kaire ir desine
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -30f + playerBody.rotation.eulerAngles.y, 30f + playerBody.rotation.eulerAngles.y);

        //Sukimasis i virsu apacia
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        float speed = Vector3.SqrMagnitude(rb.velocity) / 100;
        RotateToMatchPersonsView(speed);
    }

    public void RotateToMatchPersonsView(float speed)
    {
        float currentAngle = Quaternion.Angle(transform.rotation, playerBody.rotation);
        if (currentAngle <= thresholdAngle)
            return;
        Vector3 targetDirection = (transform.position - playerBody.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //float tempSpeed = Time.fixedDeltaTime * (((currentAngle * turnSpeed / 90)*(speed / 10))/10);
        float tempSpeed = Time.fixedDeltaTime * (currentAngle * turnSpeed / 90);
        playerBody.rotation = Quaternion.RotateTowards(playerBody.transform.rotation, transform.rotation, tempSpeed);
    }
}
