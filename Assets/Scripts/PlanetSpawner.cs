using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use the ObjectPooler to spawn a random planet
public class PlanetSpawner : MonoBehaviour
{
    public float spawnRadius = 100f;

    public int numberOfActivePlanets = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfActivePlanets; i++)
        {
            SpawnPlanet();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnPlanet()
    {
        // Get a random position inside a sphere
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 0;

        // Get a random planet from the pool
        GameObject planet = ObjectPooler.SharedInstance.GetPooledObject();

        Vector3 minSpawnRadius = transform.position * 20f;
        Vector3 maxSpawnRadius = transform.position * spawnRadius;

        planet.transform.position =  new Vector3(Random.Range(minSpawnRadius.x, maxSpawnRadius.x), 0 , Random.Range(minSpawnRadius.z, maxSpawnRadius.z));

        // Activate the planet
        planet.SetActive(true);
    }




}
