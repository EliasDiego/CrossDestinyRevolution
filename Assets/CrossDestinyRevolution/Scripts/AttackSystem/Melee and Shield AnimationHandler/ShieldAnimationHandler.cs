using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.AnimationSystem
{
    public class ShieldAnimationHandler : MonoBehaviour
    {
        [SerializeField] ActiveCharacter _activeCharacter;
        [SerializeField] AnimationEventsManager _manager;
        [SerializeField] AnimationEvent _animationEvent;
        [SerializeField] SFXAnimationEvent[] _sfx;

        private void Awake()
        {
            _animationEvent.onEventTime += PauseAnimation;
            _manager.AddAnimationEvent("Shield", _animationEvent);
            _manager.AddAnimationEvent("Shield", _sfx);
        }

        public void PlayShieldAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.Shield);
        }

        public void EndShieldAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public void PauseAnimation()
        {
            Debug.Log("shield pause");
            _activeCharacter.animator.SetFloat("ActionSMultiplier", 0);
        }

        public void ResumeAnimation()
        {
            Debug.Log("shield resume");
            _activeCharacter.animator.SetFloat("ActionSMultiplier", 1);
        }
    }
}