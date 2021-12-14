using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float damage;
    public float radius;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {
        damageObject(getObjectsInRadius());
        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, environmentParent.transform);
        Destroy(gameObject);
    }

    void damageObject(Collider[] objects)
    {
        Asteroid a;
        PlaneCombat pc;
        foreach (Collider c in objects)
        {
            a = c.GetComponent<Asteroid>();
            pc = c.GetComponentInParent<PlaneCombat>();

            if(a != null && a.health > 0)
                a.TakeDamage(damage);

            if (pc != null && pc.currentHealth > 0)
                pc.TakeDamage(damage);
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
