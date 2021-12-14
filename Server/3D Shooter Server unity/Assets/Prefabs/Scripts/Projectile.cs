using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static int nextId = 1;

    public int id;
    public int playerId;
    public int prefabId;
    public float damage;
    public float projectileForce;

    protected PlayerStatistics playerStatistics;

    [HideInInspector]
    public Vector3 targetPosition;
    float distanceToTarget = -1;

    protected void Start()
    {
        id = nextId++;
        ServerSend.ProjectileSpawned(this);
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



        
        Player p = other.GetComponentInParent<Player>();
        PlaneCombat pc = other.GetComponentInParent<PlaneCombat>();
        if (p != null && pc != null)
        {
            if (p.id != playerId)
            {
                if (pc.TakeDamage(damage))
                {
                    Debug.Log("Kill++");
                    playerStatistics.kills++;
                }
            }
        }
    }

    public void SetMetadata(int playerId, float dmg, float projectileForce)
    {
        this.playerId = playerId;
        damage = dmg;
        this.projectileForce = projectileForce;
    }

    public void SetPlayerStatistics(PlayerStatistics ps)
    {
        playerStatistics = ps;
    }

    public void SetTarget(Vector3 target)
    {
        this.targetPosition = target;
    }

    protected void DestroyOnTime(float time)
    {
        Destroy(gameObject, time);
    }
}
