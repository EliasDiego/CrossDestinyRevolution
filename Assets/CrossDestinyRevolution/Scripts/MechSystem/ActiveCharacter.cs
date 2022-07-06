using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.AttackSystem;
using CDR.MovementSystem;
using CDR.InputSystem;
using CDR.StateSystem;
using CDR.TargetingSystem;
using System;

namespace CDR.MechSystem
{
    public class ActiveCharacter : Character, IActiveCharacter
    {
        [SerializeField] Health _health;
        [SerializeField] HurtShape[] _hurtShape;
        [SerializeField] CDR.MovementSystem.CharacterController _controller;

        IInput _input;

        IState _currentState;

        [SerializeField] TargetingHandler _targetHandler;
        [SerializeField] ControllerMovement _movement;

        public Vector3 position => transform.position;
        public Quaternion rotation => transform.rotation;
        public IHealth health => _health;
        public IHurtShape[] hurtBoxes => _hurtShape;
        public ICharacterController controller => _controller;
        public IInput input { get => _input; set => _input = value; }
        public IState currentState 
        { 
            get => _currentState; 
            set
            {
                onStateChange?.Invoke(value);
                
                _currentState = value; 
            }
        }
        public ITargetHandler targetHandler => _targetHandler;
        public IMovement movement => _movement;

        public static ActiveCharacter[] activeCharacters => characterList.ToArray();

        private static List<ActiveCharacter> characterList = new List<ActiveCharacter>();

        public event Action<IState> onStateChange;

        protected override void Awake()
        {
            base.Awake();
            characterList.Add(this);
        }

        protected virtual void OnDestroy() {
            characterList.Remove(this);
        }
    }
}

