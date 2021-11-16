using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Behaviour
{
    private readonly List<T> pool = new List<T>();

    public ObjectPool(int initialPoolSize = 10)
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddObjectToPool();
        }
    }

    public T GetObject()
    {
        foreach (T pooledObject in pool)
        {
            if (!pooledObject.gameObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }

        return AddObjectToPool();
    }

    private T AddObjectToPool()
    {
        var poolGameObject = new GameObject(typeof(T).ToString());
        var pooledObject = poolGameObject.AddComponent<T>();
        poolGameObject.SetActive(false);
        pool.Add(pooledObject);
        Object.DontDestroyOnLoad(poolGameObject);
        return pooledObject;
    }
}