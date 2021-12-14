using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public Transform playerCamera;

    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
    private bool[] inputs;

    public void Initialize(int id, string username)
    {
        this.id = id;
        this.username = username;

        inputs = new bool[10];
    }

    public void FixedUpdate()
    {
        Move();
        ServerSend.PlayerStatistics(this, GetComponent<PlaneCombat>());
    }


    public bool[] getInputs()
    {
        return inputs;
    }

    private void Move()
    {
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] inputs, Quaternion rotation)
    {
        this.inputs = inputs;
        transform.rotation = rotation;
    }
}
