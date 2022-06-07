using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.ObjectPoolingSystem
{
    public interface IPoolable //attach to scripts to pool
    {
        IPool pool { get; set; }
        void ResetObject();
        void Return();
    }

    public interface IPool
    {
        void Initialize();
        void Destroy();
        void ReturnAll();
        void ReturnObject(IPoolable poolable);
        GameObject GetPoolable();
    }
}