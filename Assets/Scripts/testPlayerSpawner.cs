using System.Collections;
using PurrNet;
using PurrNet.Transports;
using UnityEngine;

public class testPlayerSpawner : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;

    private static readonly Vector3[] spawnPoints =
    {
        new Vector3(-2f, 2f, 0f),
        new Vector3(2f, 2f, 0f),
        new Vector3(-2f, -2f, 0f),
        new Vector3(2f, -2f, 0f)
    };

    private void OnEnable()
    {
        networkManager.onPlayerLoadedScene += OnPlayerLoadedScene;
    }

    private void OnDisable()
    {
        networkManager.onPlayerLoadedScene -= OnPlayerLoadedScene;
    }

    private void OnPlayerLoadedScene(PlayerID player, SceneID scene, bool asServer)
    {
        if (asServer) return;
        if (player != networkManager.localPlayer) return;

        StartCoroutine(SpawnNextFrame(player)); // part of the waiting thing down below
    }

    private IEnumerator SpawnNextFrame(PlayerID player)
    {
        yield return null; // wait one frame for other player spawning to not become weird idk

        int index = (int)player.id.value % spawnPoints.Length;
        var spawned = Instantiate(playerPrefab, spawnPoints[index], Quaternion.identity);
        var identity = spawned.GetComponent<NetworkIdentity>();
        identity.GiveOwnership(player);
    }
}