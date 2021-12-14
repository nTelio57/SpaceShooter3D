using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;

    public float radius;
    public float offsetFromPlayerRadius;
    public int minAmount;
    public int maxAmount;

    void Start()
    {
        Spawn(minAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");
            Vector3 spawnPos = getRandomPosition();
            while (Vector3.Distance(transform.position, spawnPos) < offsetFromPlayerRadius || Vector3.Distance(transform.position, spawnPos) > radius)
                spawnPos = getRandomPosition();

            GameObject ast = Instantiate(getRandomAsteroid(), spawnPos, getRandomRotation(), environmentParent.transform);
        }
    }

    Vector3 getRandomPosition()
    {
        float x = Random.Range(transform.position.x - radius, transform.position.x + radius);
        float y = Random.Range(transform.position.y - radius, transform.position.y + radius);
        float z = Random.Range(transform.position.z - radius, transform.position.z + radius);

        return new Vector3(x, y, z);
    }

    Quaternion getRandomRotation()
    {
        return Random.rotation;
    }

    GameObject getRandomAsteroid()
    {
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        return asteroidPrefabs[randomIndex];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, offsetFromPlayerRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
