using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.ObjectPoolingSystem
{
    [CreateAssetMenu(fileName = "ObjectPool", menuName = "ObjectPooling/ObjectPoolingScriptableObject", order = 1)]
    public class ObjectPooling : ScriptableObject, IPool
    {
        [SerializeField] GameObject itemToPool;

        [SerializeField]
        private List<GameObject> pooledObjects;

        public int amountToPool;
        public bool shouldExpand;

        public void Initialize()
        {
            pooledObjects = new List<GameObject>();

            //instantiate the object's prefab based on the inital amount to pool
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(itemToPool);
                //obj.GetComponent<IPoolable>().ID = _id;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPoolable()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                //we need to make sure that the object is not active
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            //if all objects are currently in use
            //check if the object can expand and then instantiate a new object and add it to the pool

            if (shouldExpand)
            {
                GameObject obj = Instantiate(itemToPool);
                //obj.GetComponent<IPoolable>().ID = item._id;
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
            return null;
        }

        public void Destroy()
        {

        }

        public void ReturnObject(IPoolable poolable)
		{

		}

        public void ReturnAll()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                //we need to make sure that the object is not active
                if (pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(false);
                }
            }
        }
    }
}

