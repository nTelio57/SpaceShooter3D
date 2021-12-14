using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float distance;
    public Vector3 pointA;
    public Vector3 pointB;

    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        pointA = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3();
        if(direction == 1)
        {
            if (Vector3.Distance(transform.position, pointB) < distance)
                direction = 2;
            newPos = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
        }
        else if (direction == 2)
        {
            if (Vector3.Distance(transform.position, pointA) < distance)
                direction = 1;
            newPos = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
        }
        
        transform.position = newPos;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("In mp");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coll entered");
        
        collision.collider.transform.parent.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trig entered");

        other.transform.parent.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Coll exited");
        collision.collider.transform.parent.SetParent(null);
    }

  
}
