using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] ItemPrefab[] itemPrefabs;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] int itemsToSpawn;

    void Start()
    {
        if(itemsToSpawn > spawnPoints.Count) throw new System.Exception();
        var spawnSpots = spawnPoints.OrderBy(x => Random.Range(0,100)).Take(itemsToSpawn).ToList();
        spawnSpots.ForEach(x => SpawnRandomItem(x.position));
    }

    private void SpawnRandomItem(Vector3 position)
    {
        ItemPrefab random = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        Instantiate(random, position, transform.rotation);
    }
}
