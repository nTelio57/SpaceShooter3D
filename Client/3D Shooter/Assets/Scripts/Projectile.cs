using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int id;
    public int playerId;
    public float damage;
    public float projectileForce;

    [HideInInspector]
    public Vector3 targetPosition;
    float distanceToTarget = -1;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        
    }

    protected void checkIfPassedTheTarget()
    {
        float newDistance = Vector3.Distance(targetPosition, transform.position);
        //Debug.Log(distanceToTarget + " : " + newDistance);
        if (distanceToTarget == -1)
        {
            distanceToTarget = newDistance;
            return;
        }
        if (newDistance > distanceToTarget)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
        distanceToTarget = newDistance;

    }

    protected void OnTriggerEnter(Collider other)
    {
        Asteroid environment = other.GetComponentInParent<Asteroid>();
        if (environment != null)
        {
            environment.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

    public void SetMetadata(float dmg)
    {
        damage = dmg;
    }

    public void SetMetadata(int playerId, float dmg, float projectileForce)
    {
        this.playerId = playerId;
        damage = dmg;
        this.projectileForce = projectileForce;
    }
    public void SetTarget(Vector3 target)
    {
        this.targetPosition = target;
    }

    protected void DestroyOnTime(float time)
    {
        Destroy(gameObject, time);
    }

    protected void OnDestroy()
    {
        GameManager.projectiles.Remove(id);
    }
}
