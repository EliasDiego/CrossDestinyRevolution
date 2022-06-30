using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace CDR.AnimationSystem
{
    public class TestAnimation : MonoBehaviour
    {
        [SerializeField]
        bool _IsMove;
        [SerializeField]
        Vector2 _Move;

        [SerializeField]
        bool _IsRangeAttack;

        [SerializeField]
        bool _UseMeleeAttackTime = false;
        [SerializeField]
        bool _IsMeleeAttack = false;
        [SerializeField]
        float _MeleeAttackTime = 0;

        Animator _Animator;

        private void Awake() 
        {
            _Animator = GetComponent<Animator>();

            AnimationEventsManager manager = GetComponent<AnimationEventsManager>();

            _Animator.SetBool("IsMove", true);

            // manager.AddAnimationEvent("Shield", new AnimationEvent(0.5f, true, Test, TestEnter, TestExit));

            // StartCoroutine(MeleeAttack());
        }

        private void Update()
        {
            _Animator.SetBool("IsRAttack", _IsRangeAttack);
        }

        private void Test()
        {
            Debug.Log("Test");
        }


        private void TestEnter()
        {
            Debug.Log("TestEnter");
        }


        private void TestExit()
        {
            Debug.Log("Testexit");
        }

        // private IEnumerator MeleeAttack()
        // {
        //     float currentTime = 0;
            
        //     if(_UseMeleeAttackTime)
        //         _Animator.SetFloat("MeleeAttackTime", 1 / _MeleeAttackTime);
        //     else
        //         _Animator.SetBool("IsMeleeAttack", true);

        //     while(currentTime < _MeleeAttackTime)
        //     {
        //         currentTime += Time.deltaTime;

        //         // _Animator.SetFloat("MeleeAttackTime", currentTime / _MeleeAttackTime);

        //         yield return null;
            
        //     }

        //     if(_UseMeleeAttackTime)
        //         _Animator.SetFloat("MeleeAttackTime", 0);
        //     else
        //         _Animator.SetBool("IsMeleeAttack", false);
        // }
    }
}