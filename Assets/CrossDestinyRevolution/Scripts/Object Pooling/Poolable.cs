using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ObjectPooling
{
    public class Poolable : MonoBehaviour
    {
        [SerializeField]
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}

