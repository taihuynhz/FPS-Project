using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] public int spawnCount = 0;
    [SerializeField] public Transform holder;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected List<GameObject> pooledObjects = new List<GameObject>();

    public virtual GameObject Spawn()
    {
        GameObject newPrefab = GetObjectFromPool(prefab);
        spawnCount++;
        return newPrefab;
    }

    protected GameObject GetObjectFromPool(GameObject prefab)
    {
        foreach (GameObject pooledObject in pooledObjects)
        {
            if (pooledObject == null) continue;
            pooledObjects.Remove(pooledObject);
            return pooledObject;
        }

        GameObject newPrefab = Instantiate(prefab, holder);
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        pooledObjects.Add(obj.gameObject);
        obj.gameObject.SetActive(false);
        spawnCount--;
    }
}
