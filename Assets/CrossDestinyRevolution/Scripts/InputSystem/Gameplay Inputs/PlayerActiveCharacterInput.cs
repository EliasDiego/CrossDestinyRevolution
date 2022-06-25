using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;
using System;

namespace CDR.InputSystem
{
    public abstract class PlayerActiveCharacterInput<T> : PlayerInput 
        where T : IActiveCharacter
    {
        private T _Character;

        private Vector3 _MovementInput;

        protected T character => _Character;

        protected Vector3 movementInput => _MovementInput;

        protected virtual void Awake()
        {
            _Character = GetComponent<T>();

            if(_Character != null)
                _Character.input = this;
        }

        private void OnChangeTarget(InputAction.CallbackContext context)
        {
            character?.targetHandler?.GetNextTarget();
                
            Debug.Log($"[Change Target Input] Changed Target");
        }


        private void OnMovement(InputAction.CallbackContext context)
        {
            _MovementInput = context.ReadValue<Vector2>();

            if(!CheckBoolean(character?.movement?.isActive))
                return;
                
            character?.movement?.Move(_MovementInput);

            Debug.Log($"[Movement Input] {_MovementInput}");
        }

        public override void AssociateActionMap(InputActionMap inputActionMap)
        {
            base.AssociateActionMap(inputActionMap);

            if (GetInputAction("Movement", out InputAction inputAction))
            {
                inputAction.performed += OnMovement;
                inputAction.canceled += OnMovement;
            }

            if (GetInputAction("ChangeTarget", out inputAction))
                inputAction.started += OnChangeTarget;
        }
    }
}