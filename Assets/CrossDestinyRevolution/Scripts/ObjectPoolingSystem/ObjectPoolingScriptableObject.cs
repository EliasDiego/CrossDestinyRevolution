using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AttackSystem;

namespace CDR.ObjectPoolingSystem
{
    [CreateAssetMenu(fileName = "ObJectToPool", menuName = "ObjectPooling/ObjectPoolingScriptableObject", order = 1)]
    public class ObjectPoolingScriptableObject : ScriptableObject
    {
        public string _id;

        public GameObject objectToPool;
        //parent transform to attach the object once instantiated
        public Transform parent;
        //how many objects will be instantiated at the beginning
        public int amountToPool;
        //if we reached the maximum amount to pool, instantiate a new prefab instance
        public bool shouldExpand;

		public void ResetObject()
		{
			//throw new System.NotImplementedException();
		}

		public void Return()
		{
			//throw new System.NotImplementedException();
		}
	}
}



