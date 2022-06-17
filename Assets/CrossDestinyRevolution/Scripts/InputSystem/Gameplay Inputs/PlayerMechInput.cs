using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;
using CDR.MovementSystem;
using CDR.AttackSystem;
using CDR.StateSystem;
using CDR.TargetingSystem;

namespace CDR.InputSystem
{
    public class PlayerMechInput : PlayerCharacterInput<IMech>
    {
        private Vector2 _MovementInput;

        private Dictionary<string, InputActionUpdate> _InputActionUpdates = new Dictionary<string, InputActionUpdate>();

        public PlayerMechInputSettings settings { get; set; }

        private class InputActionUpdate
        {
            private bool _IsUpdate;
            private Action _Action;

            public bool isUpdate => _IsUpdate;
            public Action action => _Action;

            public InputActionUpdate(InputAction inputAction, Action action)
            {
                _IsUpdate = false;
                _Action = action;
                inputAction.started += c => _IsUpdate = true;
                inputAction.canceled += c => _IsUpdate = false;
            }
        }

        private void Update()
        {
            if(!isEnabled)
                return;

            foreach(InputActionUpdate actionUpdate in _InputActionUpdates.Values)
            {
                if(actionUpdate.isUpdate)
                    actionUpdate.action?.Invoke();
            }
        }

        private bool CheckBoolean(bool? boolean)
        {
            return boolean.HasValue && boolean.Value;
        }

        private void OnSpecialAttack3(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.specialAttack3?.isActive) || CheckBoolean(character?.specialAttack3?.isCoolingDown))
                return;

            character?.specialAttack3?.Use();
                
            Debug.Log($"[Special Attack 3 Input] Used Special Attack 3!");
        }

        private void OnSpecialAttack2(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.specialAttack2?.isActive) || CheckBoolean(character?.specialAttack2?.isCoolingDown))
                return;

            character?.specialAttack2?.Use();
                
            Debug.Log($"[Special Attack 2 Input] Used Special Attack 2!");
        }

        private void OnSpecialAttack1(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.specialAttack1?.isActive) || CheckBoolean(character?.specialAttack1?.isCoolingDown))
                return;

            character?.specialAttack1?.Use();
                
            Debug.Log($"[Special Attack 1 Input] Used Special Attack 1!");
        }

        private void OnShield(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.shield?.isActive))
                return;

            character?.shield?.Use();
                
            Debug.Log($"[Shield Input] Used Shield!");
        }

        private void OnRangeAttack()
        {
            if(CheckBoolean(character?.rangeAttack?.isActive) || CheckBoolean(character?.rangeAttack?.isCoolingDown))
                return;

            character?.rangeAttack?.Use();
                
            Debug.Log($"[Range Attack Input] Used Range Attack!");
        }

        private void OnMeleeAttack(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.meleeAttack?.isActive) || CheckBoolean(character?.meleeAttack?.isCoolingDown))
                return;

            character?.meleeAttack?.Use();
                
            Debug.Log($"[Melee Attack Input] Used Melee Attack!");
        }

        private void OnChangeTarget(InputAction.CallbackContext context)
        {
            character?.targetHandler?.GetNextTarget();
                
            Debug.Log($"[Change Target Input] Changed Target");
        }

        private void OnBoostDown(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.boost?.isActive))
                return;

            float? height = character?.position.y - character?.controller?.flightPlane?.position.y;

            if(!height.HasValue)
            {
                Debug.LogAssertion($"[Boost Input Error] Character: { (character?.position).HasValue } | FlightPlane: { (character?.controller?.flightPlane?.position).HasValue }");

                return;
            }

            Debug.Log($"[Boost Input] Height : {height}");
            
            if(settings.boostInputSettings.boostUpHeightRange.IsWithinRange(height.Value))
            {
                character?.boost?.VerticalBoost(1);

                Debug.Log($"[Boost Input] Vertical Boost Up!");
            }

            else
            {
                character?.boost?.VerticalBoost(-1);

                Debug.Log($"[Boost Input] Vertical Boost Down!");
            }
        }

        // Works directional and then boost (boost and then direction is different action. Only boost is up and down so it won't work yet until boost is actually part of game object)
        private void OnBoost(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.boost?.isActive))
                return;

            if(_MovementInput.magnitude < settings.boostInputSettings.movementInputThreshold)
            {
                character?.boost?.VerticalBoost(1);

                Debug.Log($"[Boost Input] Vertical Boost Up!");
            }

            else
            {
                character?.boost?.HorizontalBoost(_MovementInput);
                
                Debug.Log($"[Boost Input] Horizontal Boost!");
            }
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

            if (GetInputAction("Boost", out inputAction))
            {
                inputAction.performed += OnBoostDown;
                inputAction.canceled += OnBoost;
            }

            if (GetInputAction("ChangeTarget", out inputAction))
                inputAction.started += OnChangeTarget;
                
            if (GetInputAction("MeleeAttack", out inputAction))
                inputAction.started += OnMeleeAttack;

            if (GetInputAction("Shield", out inputAction))
                inputAction.started += OnShield;

            if (GetInputAction("SpecialAttack1", out inputAction))
                inputAction.started += OnSpecialAttack1;
                
            if (GetInputAction("SpecialAttack2", out inputAction))
                inputAction.started += OnSpecialAttack2;
                
            if (GetInputAction("SpecialAttack3", out inputAction))
                inputAction.started += OnSpecialAttack3;

            if (GetInputAction("RangeAttack", out inputAction))
                _InputActionUpdates.Add("RangeAttack", new InputActionUpdate(inputAction, OnRangeAttack));
        }
    }
}