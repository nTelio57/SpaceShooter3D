using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float damage;
    public float radius;
    Player shooter;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode(Player player, PlayerStatistics ps)
    {
        shooter = player;
        damageObject(getObjectsInRadius(), ps);
        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, environmentParent.transform);
        Destroy(gameObject);
    }

    void damageObject(Collider[] objects, PlayerStatistics ps)
    {
        Asteroid a;
        PlaneCombat pc;
        Player otherPlayer;
        foreach (Collider c in objects)
        {
            a = c.GetComponent<Asteroid>();
            pc = c.GetComponentInParent<PlaneCombat>();
            otherPlayer = c.GetComponentInParent<Player>();

            if (a != null && a.health > 0)
                a.TakeDamage(damage);

            if (pc != null && pc.currentHealth > 0)
            {
                if (shooter == null || otherPlayer.id != shooter.id)
                {
                    if (pc.TakeDamage(damage))
                    {
                        ps.kills++;
                    }
                }
            }
        }
    }

    Collider[] getObjectsInRadius()
    {
        return Physics.OverlapSphere(transform.position, radius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
