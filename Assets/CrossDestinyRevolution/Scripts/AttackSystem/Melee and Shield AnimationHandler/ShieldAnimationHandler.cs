using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;
using CDR.VFXSystem;

namespace CDR.AnimationSystem
{
    public class ShieldAnimationHandler : MonoBehaviour
    {
        [SerializeField] ActiveCharacter _activeCharacter;
        [SerializeField] ShieldVFXHandler _vfx;
        [SerializeField] AnimationEventsManager _manager;
        [SerializeField] SFXAnimationEvent[] _sfx;

        private void Awake()
        {
            _manager.AddAnimationEvent("Shield", _sfx);
        }

        // Change Action Type
        public void PlayShieldAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.Shield);
        }

        public void EndShieldAnim()
        {
            _activeCharacter.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        // Pause/Resume Animation
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

        // Activate/Deactivate Shield
        public void ActivateShield()
        {
            _vfx.Activate();
        }

        public void DeactivateShield()
        {
            _vfx.Deactivate();
        }
    }
}