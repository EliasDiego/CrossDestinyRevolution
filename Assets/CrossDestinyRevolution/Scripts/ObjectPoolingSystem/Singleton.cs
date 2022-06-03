using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ObjectPoolingSystem
{
    public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                //if there is no instance yet,
                if (!_instance)
                {
                    //find an existing gameobject in the scene that has the generic component
                    _instance = (T)FindObjectOfType(typeof(T));
                }
                //if after checking the scene, we still don't have an instance..
                if (!_instance)
                {
                    //load a gameobject from the resources folder that has the generic component
                    //check if the system folder in the resources has the name of the generic component
                    if (Resources.Load<T>("System/" + (typeof(T)).Name) != null)
                    {
                        //if there is a prefab in the system folder,
                        //instantiate that object and set it as the instance
                        T instance = Resources.Load<T>("System/" + (typeof(T)).Name);
                        T go = (T)Instantiate(instance);
                        _instance = go;
                    }
                    else
                    {
                        _instance = null;
                    }
                }
                return _instance;
            }
        }

        [SerializeField]
        private bool isPersist = false;

        protected virtual void Awake()
        {
            //if there is no instance, set self as the instance
            if (_instance == null)
                _instance = this as T;

            //if there is already an existing instance
            if (_instance != null)
            {
                //check if self is not the actual instance
                if (_instance != this as T)
                {
                    //Destroy self because there can only be one instance
                    Destroy(this.gameObject);
                }
            }

            if (isPersist)
                DontDestroyOnLoad(this.gameObject);
        }
    }

}