using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Projectile
{
    public GameObject explosionPrefab;
    public float timeToTrigger = 3f;
    float timer = 0;

    void Start()
    {
        base.Start();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToTrigger)
            TriggerExplosion(playerStatistics.GetComponent<Player>());
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid environment = other.GetComponentInParent<Asteroid>();
        if (environment != null)
        {
            environment.TakeDamage(damage);
            TriggerExplosion(null);
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
                    playerStatistics.kills++;
                }
                TriggerExplosion(playerStatistics.GetComponent<Player>());
            }
        }
    }

    void TriggerExplosion(Player player)
    {
        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, environmentParent.transform);
        explosion.GetComponent<Explosion>().Explode(player, playerStatistics);
        Destroy(gameObject);
    }
}
