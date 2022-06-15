using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Animator _Animator;

        private void Awake() 
        {
            _Animator = GetComponent<Animator>();
        }

        private void Update() 
        {
            _Animator.SetBool("IsMove", _IsMove);
            _Animator.SetFloat("MoveX", _MoveX);
            _Animator.SetFloat("MoveY", _MoveY);    
        }
    }
}