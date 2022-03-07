using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    public class ObjectInstance
    {
        private GameObject gameObject;
        private Transform transform;
        private bool hasPoolObjectComponent;
        private PoolObject poolObjectComponent;

        public ObjectInstance(GameObject objectInstance)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);
            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectComponent = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            if (hasPoolObjectComponent)
            {
                poolObjectComponent.OnObjectReuse();
            }
            gameObject.SetActive(true);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            if (hasPoolObjectComponent)
            {
                poolObjectComponent.SetPoolParent(parent);
            }
        }

        public PoolObject GetPoolObject()
        {
            return poolObjectComponent;
        }
    }

    private Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = transform;
            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab));
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public T ReuseObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : PoolObject
    {
        int poolKey = prefab.gameObject.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation);
            return objectToReuse.GetPoolObject() as T;
        }
        return null;
    }
}
