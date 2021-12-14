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
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToTrigger)
            TriggerExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        TriggerExplosion();
    }

    void TriggerExplosion()
    {
        if (GameManager.currentGameMode == GameMode.Multiplayer)
        {
            Destroy(gameObject);
            return;
        }

        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, environmentParent.transform);
        explosion.GetComponent<Explosion>().Explode();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        base.OnDestroy();
    }
}
