using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.AnimationSystem
{
    public class MeleeAttackAnimationHandler : MonoBehaviour
    {
        [SerializeField] ActiveCharacter _activeCharacter;
        [SerializeField] AnimationEventsManager _manager;

        private void Awake()
        {
            _manager.AddAnimationEvent("MAttack", new AnimationEvent(1.0f, true, EventTime, StateEnter, StateExit));
        }

        public void PlayAttackAnim()
        {
            _activeCharacter.animator.SetBool("IsMAttack", true);
        }

        public void EndAttackAnim()
        {
            _activeCharacter.animator.SetBool("IsMAttack", false);
        }

        void EventTime()
        {
            Debug.Log("MAttack Event Time");
        }

        void StateEnter()
        {
            Debug.Log("MAttack State Enter");
        }

        void StateExit()
        {
            Debug.Log("MAttack State Exit");
        }
    }
}