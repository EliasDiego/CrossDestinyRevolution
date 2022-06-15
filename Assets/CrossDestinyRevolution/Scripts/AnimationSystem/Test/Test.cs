using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public class Test : MonoBehaviour, IAnimationEventCaller
    {
        List<Action> _AnimationEvents = new List<Action>();

        public Action[] animationEvents => _AnimationEvents.ToArray();

        // Start is called before the first frame update
        void Start()
        {
            _AnimationEvents.Add(Test1);
            _AnimationEvents.Add(Test2);
            _AnimationEvents.Add(Test3);
        }
        
        void Test1()
        {
            Debug.Log(1);
        }
        
        void Test2()
        {
            Debug.Log(2);
        }
        
        void Test3()
        {
            Debug.Log(3);
        }
    }
}