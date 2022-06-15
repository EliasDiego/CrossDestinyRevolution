using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public class Test : MonoBehaviour
    {
        AnimationEventsHolder _Holder;
        void Start()
        {
            _Holder = GetComponent<AnimationEventsHolder>();
            _Holder.AddAnimationEvent(typeof(MovementSystem.IMovement), new AnimationEvent(0.2f, true, Test1), new AnimationEvent(1f, false, Test2), new AnimationEvent(0.3f, true, Test3));
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