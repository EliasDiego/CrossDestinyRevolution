using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace CDR.AnimationSystem
{
    public class TestAnimation : MonoBehaviour
    {
        [SerializeField]
        bool _IsMove = true;
        [SerializeField, Range(-1, 1)]
        float _MoveX = 0;
        [SerializeField, Range(-1, 1)]
        float _MoveY = 0;

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

                StartCoroutine(MeleeAttack());
        }

        // private void Update() 
        // {
        //     _Animator.SetBool("IsMove", _IsMove);
        //     _Animator.SetFloat("MoveX", _MoveX);
        //     _Animator.SetFloat("MoveY", _MoveY);    
        // }

        private IEnumerator MeleeAttack()
        {
            float currentTime = 0;
            
            if(_UseMeleeAttackTime)
                _Animator.SetFloat("MeleeAttackTime", 1 / _MeleeAttackTime);
            else
                _Animator.SetBool("IsMeleeAttack", true);

            while(currentTime < _MeleeAttackTime)
            {
                currentTime += Time.deltaTime;

                // _Animator.SetFloat("MeleeAttackTime", currentTime / _MeleeAttackTime);

                yield return null;
            
            }

            if(_UseMeleeAttackTime)
                _Animator.SetFloat("MeleeAttackTime", 0);
            else
                _Animator.SetBool("IsMeleeAttack", false);
        }
    }
}