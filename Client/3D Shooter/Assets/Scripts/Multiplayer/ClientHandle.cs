using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from server: {msg}");
        Client.instance.myId = myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        try
        {
            GameManager.players[id].transform.position = position;
        }
        catch (Exception e)
        {
            Debug.Log($"Id: {id}. Players count: {GameManager.players.Count}. Player[id]: {GameManager.players[id].username}. Exception {e}. ");
        }
        //GameManager.players[id].transform.position = position;
    }

    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();
        GameManager.players[id].transform.rotation = rotation;
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();

        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
    }

    public static void AsteroidSpawn(Packet packet)
    {
        int id = packet.ReadInt();
        int skinId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        GameManager.instance.SpawnAsteroid(id, skinId, position, rotation);
    }

    public static void AsteroidTransform(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Asteroid a = GameManager.asteroids[id];
        a.transform.position = position;
        a.transform.rotation = rotation;
    }

    public static void AsteroidDestroyed(Packet packet)
    {
        int id = packet.ReadInt();

        Destroy(GameManager.asteroids[id].gameObject);
        GameManager.asteroids.Remove(id);
    }

    public static void ProjectileSpawned(Packet packet)
    {
        int projectileId = packet.ReadInt();
        int playerId = packet.ReadInt();
        int prefabId = packet.ReadInt();
        float damage = packet.ReadFloat();
        float projectileForce = packet.ReadFloat();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Vector3 target = packet.ReadVector3();

        GameManager.instance.SpawnProjectile(projectileId, playerId, prefabId, damage, projectileForce, position, rotation, target);
    }

    public static void PlayerStatistics(Packet packet)
    {
        int playerId = packet.ReadInt();
        float health = packet.ReadFloat();
        
        GameManager.players[playerId].GetComponent<PlaneCombat>().currentHealth = health;
    }

    public static void UpdateScoreboard(Packet packet)
    {
        int playerId = packet.ReadInt();
        int kills = packet.ReadInt();
        int deaths = packet.ReadInt();

        if (!(GameManager.players.ContainsKey(playerId)))
        {
            Debug.Log($"Player id:{playerId},  Count: {GameManager.players.Count}, KD: {kills}-{deaths}");
            Debug.Log("Current keys ");
            foreach (int i in GameManager.players.Keys)
                Debug.Log(i); 
        }
        GameManager.players[playerId].GetComponent<PlayerStatistics>().SetKda(kills, deaths);
    }
}
