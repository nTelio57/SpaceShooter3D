using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;

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

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50, 5757);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer()
    {
        float respawnRange = 500f;
        float x, y, z;
        x = Random.Range(-respawnRange, respawnRange);
        y = Random.Range(-respawnRange, respawnRange);
        z = Random.Range(-respawnRange, respawnRange);
        Vector3 spawnPos = new Vector3(x, y, z);
        return Instantiate(playerPrefab, spawnPos, Random.rotation).GetComponent<Player>();
    }
}
