using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ObjectPoolingSystem
{
    public interface IPoolable
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

        void Initialize();
        void Destroy();
        IPoolable GetPoolable();
        IPoolable[] GetPoolables();
    }
}