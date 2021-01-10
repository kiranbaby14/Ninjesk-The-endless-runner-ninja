using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public float zSpawn = 50;
    public float EnemyLength = 25;
    public int numberOfEnemies = 5;
    private List<GameObject> activeEnemies = new List<GameObject>();


    public Transform playerTransform;
    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnTile(Random.Range(0, EnemyPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 25 > zSpawn - (numberOfEnemies * EnemyLength))
        {
            SpawnTile(Random.Range(0, EnemyPrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(EnemyPrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeEnemies.Add(go);
        zSpawn += EnemyLength;
    }

    private void DeleteTile()
    {
        Destroy(activeEnemies[0]);
        activeEnemies.RemoveAt(0);
    }
}
