using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int id;
    public Rigidbody rb;
    public float health = 50;
    [Header("Random fly direction")]
    public bool isRandomMass;
    public float minRandomMass = 50f;
    public float maxRandomMass = 150f;
    [Header("Random fly direction")]
    public bool isFlyingToRandomDirection;
    public float minRandomSpeed = 100f;
    public float maxRandomSpeed = 100f;
    public GameObject explosionPrefab;

    void Start()
    {
        if (isRandomMass)
            setRandomMass();
        if (isFlyingToRandomDirection)
            setRandomDirection();
    }

    
    void FixedUpdate()
    {
        //rb.AddForce(rb.velocity * Time.fixedDeltaTime);
    }

    void setRandomMass()
    {
        rb.mass = Random.Range(minRandomMass, maxRandomMass);
    }

    void setRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        float speed = Random.Range(minRandomSpeed, maxRandomSpeed);
        rb.AddForce(new Vector3(x, y, z) * speed * 10 * rb.mass);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        TriggerExplosion();
    }

    void TriggerExplosion()
    {
        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, environmentParent.transform);
        explosion.GetComponent<Explosion>().Explode();
        GameManager.asteroids.Remove(id);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.currentGameMode == GameMode.Multiplayer)
            return;

        PlaneCombat pc = collision.collider.GetComponentInParent<PlaneCombat>();
        Rigidbody planeRb = collision.collider.GetComponentInParent<Rigidbody>();

        if (pc != null && planeRb != null)
            TakeDamage(planeRb.mass);
    }

}
