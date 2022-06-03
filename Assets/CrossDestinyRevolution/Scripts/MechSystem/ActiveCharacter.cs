using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.AttackSystem;
using CDR.MovementSystem;
using CDR.InputSystem;
using CDR.StateSystem;
using CDR.TargetingSystem;
using CDR.HitboxSystem;

namespace CDR.MechSystem
{
    public class ActiveCharacter : Character, IActiveCharacter
    {
        [SerializeField] Health _health;
        [SerializeField] HurtBox[] _hurtBoxes;

        ICharacterController _controller;

        [SerializeField] IInput _input;

        IState _currentState;

        [SerializeField] ITargetHandler _targetHandler;
        [SerializeField] IMovement _movement;


        public Vector3 position => transform.position;

        public IHealth health => _health;

        public HitboxSystem.IHurtBox[] hurtBoxes => _hurtBoxes;

        public ICharacterController controller => _controller;

        public IInput input { get => _input; set => _input = value; }
        public IState currentState { get => _currentState; set => _currentState = value; }

        public ITargetHandler targetHandler => _targetHandler;

        public IMovement movement => _movement;

        public static ActiveCharacter[] activeCharacters => characterList.ToArray();
        private static List<ActiveCharacter> characterList = new List<ActiveCharacter>();

        protected override void Awake()
        {
            base.Awake();
            characterList.Add(this);
            _controller = GetComponent<ICharacterController>();
            _targetHandler = GetComponent<ITargetHandler>();
            _movement = GetComponent<IMovement>();
        }

        protected virtual void OnDestroy() {
            characterList.Remove(this);
        }
    }
}

