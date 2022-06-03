using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.ObjectPoolingSystem
{
    public class ObjectPooling : Singleton<ObjectPooling>, IPool
    {
        public List<ObjectPoolingScriptableObject> itemsToPool;

        [SerializeField]
        private List<GameObject> pooledObjects;

        public IActiveCharacter targetCharacter => GetComponent<ActiveCharacter>();

        public virtual void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            pooledObjects = new List<GameObject>();

            foreach (ObjectPoolingScriptableObject item in itemsToPool)
            {
                //instantiate the object's prefab based on the inital amounttopool
                for (int i = 0; i < item.amountToPool; i++)
                {
                    //Set Object Owner

                    //Instantiate the prefab
                    GameObject obj = Instantiate(item.objectToPool, item.parent);
                    obj.GetComponent<IPoolable>().ID = item._id;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPoolable(string _id)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                //we need to make sure that the object is not active
                if (!pooledObjects[i].activeInHierarchy &&
                    pooledObjects[i].GetComponent<IPoolable>().ID == _id)
                {
                    return pooledObjects[i];
                }
            }

            //if all objects are currently in use
            //check if the object can expand and then instantiate a new object and add it to the pool
            foreach (ObjectPoolingScriptableObject item in itemsToPool)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, item.parent);
                    obj.GetComponent<IPoolable>().ID = item._id;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
            return null;
        }

        public void Destroy()
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

