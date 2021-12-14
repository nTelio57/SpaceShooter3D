using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCombat : MonoBehaviour
{
    //public Transform target;
    public Target target;
    public Transform cameraTransform;
    [HideInInspector]
    public float currentHealth = 500;
    public float maxHealth = 500;
    public float armor = 35;

    public GunType[] gunTypes;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentGameMode == GameMode.Multiplayer)
            return;
        foreach (GunType gt in gunTypes)
        {
            gt.attackTimer += Time.deltaTime;
            if (Input.GetKey(gt.buttonIndex) && gt.attackTimer >= (1 / gt.attackSpeed))
            {
                Shoot(gt);
                gt.attackTimer = 0;
            }
        }
    }

    void Shoot(GunType gunType)
    {
        foreach (Transform fp in gunType.firePoints)
        {
            GameObject bulletPrefab = Instantiate(gunType.bulletPrefab, fp.position, cameraTransform.rotation);
            Projectile bullet = bulletPrefab.GetComponent<Projectile>();
            bullet.SetMetadata(gunType.attackDamage);
            bullet.SetTarget(target.currentTarget);
            //bullet.SetTarget(target.getShootingVector()); ;
            Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
            rb.AddForce((target.currentTarget - fp.position).normalized * gunType.bulletForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(float amount)
    {
        //health -= Mathf.Clamp((amount - armor), 0, (amount - armor));
        currentHealth -= (amount - armor);
        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Respawn();
    }

    public void Respawn()
    {
        float respawnRange = 500f;
        currentHealth = maxHealth;
        float x, y, z;
        x = Random.Range(-respawnRange, respawnRange);
        y = Random.Range(-respawnRange, respawnRange);
        z = Random.Range(-respawnRange, respawnRange);
        transform.position = new Vector3(x, y, z);
        transform.rotation = Random.rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Asteroid a = collision.collider.GetComponent<Asteroid>();
        if (a != null)
        {
            TakeDamage(a.rb.mass);
        }
    }
}
