using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerCamera;
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.LeftControl),
            Input.GetKey(KeyCode.Space),
            Input.GetKeyDown(KeyCode.LeftShift),
            Input.GetKey(KeyCode.LeftShift),
            Input.GetKey(KeyCode.Mouse0),
            Input.GetKey(KeyCode.Mouse1)
        };

        ClientSend.PlayerInput(inputs, playerCamera.rotation);
    }
}
