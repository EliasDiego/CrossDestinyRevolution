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
        [SerializeField] AnimationEvent _pauseEvent;
        [SerializeField] SFXAnimationEvent _sfx;

        private void Awake()
        {
            _pauseEvent.onEventTime += PauseAnimation;
            _manager.AddAnimationEvent("MAttack", _pauseEvent);
            _manager.AddAnimationEvent("MAttack", _sfx);
        }

        // Change Action Type
        public void PlayAttackAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.MeleeAttack);
        }

        public void EndAttackAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        // Pause/Resume Animation
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
    }
}