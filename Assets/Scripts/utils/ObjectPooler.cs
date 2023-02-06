using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create an object pooler that can contain different types of pools (planets, asteroids, etc) using a dictionary and can be used to spawn them

public class ObjectPooler : MonoBehaviour
{
    // Create a singleton
    public static ObjectPooler SharedInstance;

    // Create a dictionary to store the different pools
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Create a list of pools
    public List<Pool> pools;

    // Create a class to store the different pools
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    // Start is called before the first frame update
    void Awake()
    {
        SharedInstance = this;
        // Create a new dictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
         // Loop through the list of pools
        foreach (Pool pool in pools)
        {
            // Create a new queue for each pool
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // Loop through the size of the pool
            for (int i = 0; i < pool.size; i++)
            {
                // Create a new object from the prefab
                GameObject obj = Instantiate(pool.prefab);

                // Deactivate the object
                obj.SetActive(false);
                // Add the object to the queue
                objectPool.Enqueue(obj);
            }

            // Add the queue to the dictionary
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    void Start()
    {
       
    }

    // Get a pooled object from the dictionary
    public GameObject GetPooledObject(string tag)
    {
        // Check if the dictionary contains the tag
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Get the first object from the queue
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Add the object to the end of the queue
        poolDictionary[tag].Enqueue(objectToSpawn);

        // Return the object
        return objectToSpawn;
    }
}