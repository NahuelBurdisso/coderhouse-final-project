using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use the ObjectPooler to spawn a random planet
public class PlanetSpawner : MonoBehaviour
{
    public float spawnRadius;

    public int numberOfActivePlanets;

    private Vector3[] spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3[numberOfActivePlanets];

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
        // Get a random planet from the pool
        GameObject planet = ObjectPooler.SharedInstance.GetPooledObject("Planet");

        if (planet == null)
        {
            Debug.LogWarning("No planet found in the pool");
            return;
        }

        // Set the position of the planet using the planet spawnner position as the center and the spawn radius 
        // to get a random position inside a sphere starting from there but always maintaining the same y position in 0
        Vector3 randomPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

        // make sure the planet is not spawned inside another planet
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            if (Vector3.Distance(randomPosition, spawnPosition[i]) < 10)
            {
                randomPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;
            }
        }

        planet.transform.position = randomPosition;

        // add the planet position to the array
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            if (spawnPosition[i] == Vector3.zero)
            {
                spawnPosition[i] = randomPosition;
                break;
            }
        }

        // Activate the planet
        planet.SetActive(true);
    }


    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }



}
