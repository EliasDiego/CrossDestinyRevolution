using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.VersusSystem
{
    public class PlayerDeviceInput : PlayerUIInput, IPoolable
    {
        public IPool pool { get; set; }

        public void ResetObject()
        {
            
        }

        public void Return()
        {
            pool.ReturnObject(this);
        }
    }
}