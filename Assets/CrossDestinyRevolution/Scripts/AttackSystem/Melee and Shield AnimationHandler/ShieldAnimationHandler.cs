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

        private void Awake()
        {
            _manager.AddAnimationEvent("Shield", new AnimationEvent(0.5f, true, EventTime, StateEnter, StateExit));
        }

        public void PlayShieldAnim()
        {
            _activeCharacter.animator.SetBool("IsShield", true);
            //_activeCharacter.animator.SetFloat("ShieldSMultiplier", 0);
        }

        public void EndShieldAnim()
        {
            _activeCharacter.animator.SetBool("IsShield", false);
        }

        void EventTime()
        {
            Debug.Log("Shield Event Time");
        }

        void StateEnter()
        {
            Debug.Log("Shield State Enter");
        }

        void StateExit()
        {
            Debug.Log("Shield State Exit");
        }
}
}