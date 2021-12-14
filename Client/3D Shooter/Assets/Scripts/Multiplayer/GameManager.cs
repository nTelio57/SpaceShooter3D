using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{ 
    None,
    Singleplayer,
    Multiplayer
}

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public static GameMode currentGameMode = GameMode.None;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, Asteroid> asteroids = new Dictionary<int, Asteroid>();
    public static Dictionary<int, Projectile> projectiles = new Dictionary<int, Projectile>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject[] asteroidPrefabs;
    public GameObject[] projectilePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public static void Disconnect()
    { 
        players = new Dictionary<int, PlayerManager>();
        asteroids = new Dictionary<int, Asteroid>();
    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        GameObject player;
        if (id == Client.instance.myId)
        {
            player = Instantiate(localPlayerPrefab, position, rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, position, rotation);
        }

        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id, player.GetComponent<PlayerManager>());
    }

    public void SpawnAsteroid(int id, int skinId, Vector3 position, Quaternion rotation)
    {
        GameObject environmentParent = GameObject.FindGameObjectWithTag("Environment");

        GameObject ast = Instantiate(asteroidPrefabs[skinId], position, rotation, environmentParent.transform);
        ast.GetComponent<Asteroid>().id = id;
        asteroids.Add(id, ast.GetComponent<Asteroid>());
    }

    public void SpawnProjectile(int projectileId, int playerId, int prefabId, float damage, float projectileForce, Vector3 position, Quaternion rotation, Vector3 targetPosition)
    {
        GameObject bulletPrefab = Instantiate(projectilePrefab[prefabId], position, rotation);
        Projectile bullet = bulletPrefab.GetComponent<Projectile>();
        bullet.SetMetadata(playerId, damage, projectileForce);
        bullet.SetTarget(targetPosition);
        bullet.id = projectileId;
        Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
        rb.AddForce((targetPosition - position).normalized * projectileForce, ForceMode.Impulse);

        projectiles.Add(projectileId, bullet);
    }
}
