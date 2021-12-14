using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class ServerSend
{
    private static void SendTCPData(int toClient, Packet packet)
    {
        packet.WriteLength();
        Server.clients[toClient].tcp.SendData(packet);
    }

    private static void SendUDPData(int toClient, Packet packet)
    {
        packet.WriteLength();
        Server.clients[toClient].udp.SendData(packet);
    }

    private static void SendTCPDataToAll(Packet packet)
    {
        packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(packet);
        }
    }

    private static void SendUDPDataToAll(int exceptClient, Packet packet)
    {
        packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != exceptClient)
            {
                Server.clients[i].udp.SendData(packet);
            }
        }
    }

    private static void SendUDPDataToAll(Packet packet)
    {
        packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(packet);
        }
    }

    private static void SendTCPDataToAll(int exceptClient, Packet packet)
    {
        packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != exceptClient)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }
    }

    #region Packets
    public static void Welcome(int toClient, string msg)
    {
        using (Packet packet = new Packet((int)ServerPackets.welcome))
        {
            packet.Write(msg);
            packet.Write(toClient);

            SendTCPData(toClient, packet);
        }
    }

    public static void SpawnPlayer(int toClient, Player player)
    {
        using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            packet.Write(player.id);
            packet.Write(player.username);
            packet.Write(player.transform.position);
            packet.Write(player.transform.rotation);

            SendTCPData(toClient, packet);
        }
    }

    public static void PlayerPosition(Player player)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerPosition))
        {
            packet.Write(player.id);
            packet.Write(player.transform.position);

            SendUDPDataToAll(packet);
        }
    }

    public static void PlayerRotation(Player player)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerRotation))
        {
            packet.Write(player.id);
            packet.Write(player.transform.rotation);

            SendUDPDataToAll(player.id, packet);
        }
    }

    public static void PlayerDisconnected(int playerId)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerDisconnected))
        {
            packet.Write(playerId);

            SendTCPDataToAll(packet);
        }
    }

    public static void AsteroidSpawn(Asteroid asteroid)
    {
        using (Packet packet = new Packet((int)ServerPackets.asteroidSpawn))
        {
            packet.Write(asteroid.id);
            packet.Write(asteroid.skinId);
            packet.Write(asteroid.transform.position);
            packet.Write(asteroid.transform.rotation);

            SendTCPDataToAll(packet);
        }
    }

    public static void AsteroidTransform(Asteroid asteroid)
    {
        using (Packet packet = new Packet((int)ServerPackets.asteroidTransform))
        {
            packet.Write(asteroid.id);
            packet.Write(asteroid.transform.position);
            packet.Write(asteroid.transform.rotation);

            SendUDPDataToAll(packet);
        }
    }

    public static void AsteroidDestroyed(Asteroid asteroid)
    {
        using (Packet packet = new Packet((int)ServerPackets.asteroidDestroyed))
        {
            packet.Write(asteroid.id);

            SendTCPDataToAll(packet);
        }
    }

    public static void ProjectileSpawned(Projectile projectile)
    {
        using (Packet packet = new Packet((int)ServerPackets.projectileSpawn))
        {
            packet.Write(projectile.id);
            packet.Write(projectile.playerId);
            packet.Write(projectile.prefabId);
            packet.Write(projectile.damage);
            packet.Write(projectile.projectileForce);
            packet.Write(projectile.transform.position);
            packet.Write(projectile.transform.rotation);
            packet.Write(projectile.targetPosition);

            SendTCPDataToAll(packet);
        }
    }

    public static void PlayerStatistics(Player p, PlaneCombat pc)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerStatistics))
        {
            packet.Write(p.id);
            packet.Write(pc.currentHealth);

            SendTCPData(p.id, packet);
        }
    }

    public static void UpdateScoreboard(int playerId, PlayerStatistics ps)
    {
        using (Packet packet = new Packet((int)ServerPackets.updateScoreboard))
        {
            packet.Write(playerId);
            packet.Write(ps.kills);
            packet.Write(ps.deaths);

            SendTCPDataToAll(packet);
        }
    }
    #endregion
}
