using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCombat : MonoBehaviour
{
    public Player p;
    //public Transform target;
    public Target target;
    public Transform cameraTransform;
    //[HideInInspector]
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
        foreach (GunType gt in gunTypes)
        {
            gt.attackTimer += Time.deltaTime;
            if (GetShootKey(gt.buttonIndex) && gt.attackTimer >= (1 / gt.attackSpeed))
            {
                Shoot(gt);
                gt.attackTimer = 0;
            }
        }
    }

    bool GetShootKey(KeyCode keyCode)
    {
        bool[] inputs = p.getInputs();
        if (inputs[8] && keyCode == KeyCode.Mouse0)
            return true;
        if (inputs[9] && keyCode == KeyCode.Mouse1)
            return true;
        return false;
    }

    void Shoot(GunType gunType)
    {
        foreach (Transform fp in gunType.firePoints)
        {
            GameObject bulletPrefab = Instantiate(gunType.bulletPrefab, fp.position, cameraTransform.rotation);
            Projectile bullet = bulletPrefab.GetComponent<Projectile>();
            bullet.SetMetadata(GetComponent<Player>().id, gunType.attackDamage, gunType.bulletForce);
            bullet.SetTarget(target.currentTarget);
            bullet.SetPlayerStatistics(GetComponent<PlayerStatistics>());
            Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
            Vector3 forceToAdd = Vector3.zero;
            if (target.averageDistance != -1)
                forceToAdd = ((target.currentTarget - fp.position).normalized * (target.averageDistance * 1.12f)).normalized * gunType.bulletForce;
            else
                forceToAdd = (target.currentTarget - fp.position).normalized * gunType.bulletForce;
            //Debug.Log("Force to addd " + forceToAdd + "   " + Vector3.SqrMagnitude(forceToAdd));
            rb.AddForce(forceToAdd, ForceMode.Impulse);
        }
    }

    public bool TakeDamage(float amount)
    {
        //health -= Mathf.Clamp((amount - armor), 0, (amount - armor));
        currentHealth -= (amount - armor);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Rturn true");
            return true;
        }
        return false;
    }

    public void Die()
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
        GetComponent<PlayerStatistics>().deaths++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Asteroid a = collision.collider.GetComponent<Asteroid>();
        if (a != null)
        {
            Debug.Log("Mass "+a.rb.mass);
            TakeDamage(a.rb.mass);
        }
    }
}
