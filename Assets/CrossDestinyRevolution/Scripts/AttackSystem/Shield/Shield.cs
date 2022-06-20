using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.ActionSystem;
using CDR.StateSystem;
using CDR.MechSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class Shield : Action, IShield
    {
        [SerializeField] HurtSphere _hurtSphere;
        [SerializeField] SphereCollider _sphereCollider;
        [SerializeField] float _radius;

        // Object Pool
        [SerializeField] ObjectPooling _pool;

        // State
        IMech sender;
        IMech receiver;

        [SerializeField] bool _isShieldActive;

        public float radius => _radius;
        public IHurtShape hurtSphere => _hurtSphere;


        protected override void Awake()
        {
            base.Awake();

            if(_pool != null)
                _pool.Initialize();
        }

        private void Start()
        {
            _sphereCollider.radius = radius;
        }

        public override void Use()
        {
            base.Use();

            _sphereCollider.enabled = true;
            _hurtSphere.onHitEnter += HitEnter;

            Character.input.DisableInput();
            Character.movement.End();
        }

        public override void End()
        {
            base.End();

            _sphereCollider.enabled = false;
            _hurtSphere.onHitEnter -= HitEnter;

            Character.input.EnableInput();
            Character.movement.Use();
        }

        public override void ForceEnd()
        {
            base.ForceEnd();

            _sphereCollider.enabled = false;
            _hurtSphere.onHitEnter -= HitEnter;
        }

        void HitEnter(IHitEnterData hitData)
        {
            Debug.LogWarning("Hit Enter from: " + hitData.hitShape.character);
            
            sender = (IMech)Character;
            receiver = (IMech)hitData.hitShape.character;

            GameObject stun = _pool.GetPoolable();
            stun.transform.SetParent(((ActiveCharacter)receiver).transform);
			stun.SetActive(true);

            receiver.currentState = stun.GetComponent<IState>();
			receiver.currentState.sender = sender;
			receiver.currentState.receiver = receiver;

			receiver.currentState.StartState();
        }
    }
}
