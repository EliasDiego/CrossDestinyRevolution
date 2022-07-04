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
        [SerializeField] AnimationEvent _animationEvent;
        [SerializeField] SFXAnimationEvent _sfx;

        private void Awake()
        {
            var a = new AnimationEvent(.2f, true, () => PauseAnimation());

            _manager.AddAnimationEvent("MAttack", a);
            _manager.AddAnimationEvent("MAttack", _sfx);
        }

        public void PlayAttackAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.MeleeAttack);
        }

        public void EndAttackAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public void PauseAnimation()
        {
            Debug.Log("melee paused");
            _activeCharacter.animator.SetFloat("ActionSMultiplier", 0);
        }

        public void ResumeAnimation()
        {
            Debug.Log("melee resumed");
            _activeCharacter.animator.SetFloat("ActionSMultiplier", 1);
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