using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using Fusion.Sockets;
using System;
using System.Collections;

public class FusionLauncher : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform fixedSpawnPoint;

    private NetworkRunner runner;

    public async void StartHost()
    {
        runner = GetComponent<NetworkRunner>();
            
        runner.ProvideInput = true;
        
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host, // For LAN play we'll start with Host mode
            SessionName = "LAN_Mario",
            Scene       = SceneRef.FromIndex(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public async void StartClient()
    {
        
    }

    public void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        //Vector3 spawnPos = fixedSpawnPoint.position; // empty object assigned in inspector
        Vector3 spawnPos = fixedSpawnPoint != null ? fixedSpawnPoint.position : Vector3.zero;
        runner.Spawn(playerPrefab, spawnPos, Quaternion.identity, player);
    }
}

