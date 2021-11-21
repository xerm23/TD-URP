using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    public string tag; //used as a key in dictionary 
    public GameObject prefab; //object to spawn
    public int size; //amount of object to spawn
}

/// <summary>
/// script spawns all objects from pools on awake and sets them inactive
/// each object can be activated later from pool via SpawnFromPool function
/// pooling system works as FIFO that's why Queue is used in pool dictionary
/// </summary>

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools = new List<Pool>();

    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parentGO = new GameObject();
            parentGO.transform.parent = this.transform;
            parentGO.name = pool.tag;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parentGO.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log($"Pool with tag {tag} doesn't exist.");
            return null;
        }    
        //first object of queue
        GameObject objectToSpawn =  poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        //spawned -> becomes the last one in the queue
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
