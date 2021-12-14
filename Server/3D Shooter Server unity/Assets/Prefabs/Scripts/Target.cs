using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform farTarget;
    public Vector3 currentTarget;
    public float distance = -1;
    public int rowCount;
    public int columnCount;
    public float distanceOffset = 100;
    public float offsetBetweenColumn = 10;
    public float offsetBetweenRow = 10;
    public float averageDistance = -1;

    float alpha;
    float distanceToFarTarget;
    RaycastHit hit;

    void Start()
    {
        currentTarget = farTarget.position;
        distanceToFarTarget = Vector3.Distance(transform.position, farTarget.position);
    }


    void Update()
    {
        Vector3 direction = (farTarget.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if(hit.transform.tag != "Bullet")
            {
                currentTarget = hit.point;
                distance = Vector3.Distance(transform.position, currentTarget);
            }
        }
        else
        {
            distance = -1;
            currentTarget = farTarget.position;
        }

        averageDistance = CalculateAverageDistance();  
        
    }

    float CalculateAverageDistance()
    {
        float width = (columnCount - 1) * offsetBetweenColumn;
        float height = (rowCount - 1) * offsetBetweenRow;

        Vector3 start = farTarget.position + (-farTarget.up) * (height / 2) - (farTarget.right) * (width / 2);

        Vector3 stepRight = farTarget.right * offsetBetweenColumn;
        Vector3 stepUp = farTarget.up * offsetBetweenRow;

        Vector3 direction, iterVector;
        RaycastHit iterHit;
        float distanceSum = 0;
        int distanceCount = 0;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                iterVector = start + stepRight * j + stepUp * i;
                direction = (iterVector - transform.position).normalized;

                if (Physics.Raycast(transform.position, direction, out iterHit))
                {
                    if (iterHit.transform.tag != "Bullet")
                    {
                        distanceSum += Vector3.Distance(transform.position, iterHit.point);
                        distanceCount++;
                    } 
                }
            }
        }

        if (distanceCount > 0)
            return distanceSum / distanceCount;
        else
            return -1;
    }

    public Vector3 getShootingVector()
    {
        if (averageDistance != -1)
            return currentTarget.normalized * (averageDistance + distanceOffset);
        else
            return currentTarget;
    }

   /* private void OnDrawGizmos()
    {
        float width = (columnCount - 1) * offsetBetweenColumn;
        float height = (rowCount - 1) * offsetBetweenRow;

        Vector3 start = farTarget.position + (-farTarget.up) * (height / 2) - (farTarget.right) * (width / 2);

        Vector3 stepRight = farTarget.right * offsetBetweenColumn;
        Vector3 stepUp = farTarget.up * offsetBetweenRow;
        Vector3 iterVector;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                iterVector = start + stepRight * j + stepUp * i;
                Gizmos.DrawLine(transform.parent.position, iterVector);
            }
        }
    }*/
}
