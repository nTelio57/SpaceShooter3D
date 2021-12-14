using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public Transform target;
    public KeyCode activationKey;
    public bool isTurnedOn = false;
    public float damagePerSecond = 7;
    public float maxEnergy = 100;
    public float energyCostPerSecond = 7;
    public float energyRestorePerSecond = 5;
    [HideInInspector]
    public float currentEnergy;
    
    void Start()
    {
        currentEnergy = maxEnergy * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        toggle();
        manageEnergy();

        if (!isTurnedOn)
            return;

        drawLaser();
    }

    void manageEnergy()
    {
        if (isTurnedOn)
        {
            currentEnergy -= energyCostPerSecond * Time.deltaTime;
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isTurnedOn = false;
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

    void doDamage(Collider collider)
    {
        Asteroid asteroid = collider.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }

    void drawLaser()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                doDamage(hit.collider);
            }
        }
        else
            lr.SetPosition(1, target.position);
    }

    void toggle()
    {
        if (isTurnedOn)
        {
            if (Input.GetKeyDown(activationKey))
            {
                isTurnedOn = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(activationKey))
            {
                isTurnedOn = true;
            }
        }

        lr.enabled = isTurnedOn;
    }
}
