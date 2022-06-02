using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ObjectPoolingSystem
{
    public interface IPoolable //attach to scripts to pool
    {
        IPool pool { get; }

        void ResetObject();
        void Return();
    }

    public interface IPool
    {
        IPoolable poolable { get; }
        int poolSize { get; }
        int activePoolables { get; }
        Transform parent { get; }

        public GameObject objectToPool { get;}

        bool shouldExpand { get; }

        void Initialize();
        void Destroy();

        IPoolable GetPoolable();
        IPoolable[] GetPoolables();
    }
}