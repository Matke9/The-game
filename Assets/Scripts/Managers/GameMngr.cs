using MLAPI;
using MLAPI.Connection;
using MLAPI.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : NetworkBehaviour
{
    [SerializeField]
    public GameObject MovingPlayerPrefab;
    public GameObject[] Cities;

    private int i = 0;

    private Dictionary<ulong, bool> playerReady = new Dictionary<ulong, bool>();

    public void Start()
    {
        Cities = GameObject.FindGameObjectsWithTag("City");
        OnPlayerReadyServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    public override void NetworkStart()
    {
        foreach (KeyValuePair<ulong, NetworkClient> nc in NetworkManager.Singleton.ConnectedClients)
        {
            playerReady.Add(nc.Key, false);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void OnPlayerReadyServerRpc(ulong clientId)
    {
        // Called when the player is done loading his scene, and his network is initialized
        playerReady[clientId] = true;
        Debug.Log(clientId);
        // If all players are ready, spawn all the networked pieces
        if (!playerReady.ContainsValue(false))
            SpawnPlayers();
    }
    public void SpawnPlayers()
    {
        foreach (KeyValuePair<ulong, NetworkClient> nc in NetworkManager.Singleton.ConnectedClients)
        {
            Cities[i].GetComponent<CityData>().OwnerID = Convert.ToInt32(nc.Key);
            GameObject p = Instantiate(MovingPlayerPrefab,Cities[i].transform.GetChild(0).position,new Quaternion(0,0,0,0));
            p.GetComponent<NetworkObject>().SpawnWithOwnership(nc.Key);
            i++;
        }
    }
}
