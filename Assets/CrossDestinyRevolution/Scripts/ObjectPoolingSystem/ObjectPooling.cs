using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ObjectPooling
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        //unique identifier
        public string id;
        //prefab
        public GameObject objectToPool;
        //parent transform to attach the object once instantiated
        public Transform parent;
        //how many objects will be instantiated at the beginning
        public int amountToPool;
        //if we reached the maximum amount to pool, instantiate a new prefab instance
        public bool shouldExpand;

    }

    public class ObjectPooling : MonoBehaviour
    {
        public List<ObjectPoolItem> itemsToPool;
        [SerializeField]
        private List<GameObject> pooledObjects;

        public void PrintConsole()
        {
            Debug.Log("I AM THE OBJECT POOL MANAGER");
        }


        private void Start()
        {
            pooledObjects = new List<GameObject>();

            //Traverse through each objectpoolitem in the list
            foreach (ObjectPoolItem item in itemsToPool)
            {
                //instantiate the object's prefab based on the inital amounttopool
                for (int i = 0; i < item.amountToPool; i++)
                {
                    //Instantiate the prefab
                    GameObject obj = Instantiate(item.objectToPool, item.parent);
                    obj.AddComponent<Poolable>();
                    obj.GetComponent<Poolable>().ID = item.id;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string id)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                //we need to make sure that the object is not active
                //and that the object has the same id
                if (!pooledObjects[i].activeInHierarchy &&
                    pooledObjects[i].GetComponent<Poolable>().ID == id)
                {
                    return pooledObjects[i];
                }
            }

            //if all objects are currently in use
            //check if the object can expand and then instantiate a new object and add it to the pool
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.id == id)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = Instantiate(item.objectToPool, item.parent);
                        obj.AddComponent<Poolable>();
                        obj.GetComponent<Poolable>().ID = item.id;
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}