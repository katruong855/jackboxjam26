using System.Collections;
using UnityEngine;
using PurrNet;

public class testAnimalSpawner : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private int animalCount = 6;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-4f, -4f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(4f, 4f);

    private void OnEnable()
    {
        networkManager.onNetworkStarted += OnNetworkStarted;
    }

    private void OnDisable()
    {
        networkManager.onNetworkStarted -= OnNetworkStarted;
    }

    private void OnNetworkStarted(NetworkManager manager, bool asServer)
    {
        if (!asServer) return;
        StartCoroutine(SpawnAnimalsNextFrame());
    }

    private IEnumerator SpawnAnimalsNextFrame()
    {
        yield return null;

        for (int i = 0; i < animalCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f
            );
            Instantiate(animalPrefab, pos, Quaternion.identity);
        }
    }
}