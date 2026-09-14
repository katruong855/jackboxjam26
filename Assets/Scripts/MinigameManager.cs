using UnityEngine;
using PurrNet;
using TMPro;

public class MinigameManager : NetworkBehaviour
{
    // [SerializeField] private NetworkManager networkManager;
    // [SerializeField] private GameObject timer;

    // private void OnEnable()
    // {
    //     networkManager.onPlayerLoadedScene += OnPlayerLoadedScene;
    // }

    // private void OnDisable()
    // {
    //     networkManager.onPlayerLoadedScene -= OnPlayerLoadedScene;
    // }

    // private void OnPlayerLoadedScene(PlayerID player, SceneID scene, bool asServer)
    // {
    //     if (asServer) return;
    //     if (player != networkManager.localPlayer) return;

    //     var spawned = Instantiate(playerPrefab);
    //     var identity = spawned.GetComponent<NetworkIdentity>();
    //     identity.GiveOwnership(player);
        
    // }
}
