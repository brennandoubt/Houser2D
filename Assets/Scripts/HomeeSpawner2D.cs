using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeeSpawner2D : MonoBehaviour
{
    public float spawnWidth = 1;
    public float spawnRate = 1;
    private float lastSpawnTime = 0;


    public GameObject HomeePrefab;

    // Update is called once per frame
    void Update()
    {
        // this is a simple timer structure that executes every 1/spawnRate seconds. This means it spawns spawnRate homees every second.
        if (lastSpawnTime + 1 / spawnRate < Time.time)
        {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
            // the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
            Instantiate(HomeePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
