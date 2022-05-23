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

        private Dictionary<string, InputAction> _InputActions = new Dictionary<string, InputAction>();
        private Dictionary<string, InputActionUpdate> _InputActionUpdates = new Dictionary<string, InputActionUpdate>(); 

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
            if(CheckBoolean(character?.specialAttack3?.isActive) && CheckBoolean(character?.specialAttack3?.isCoolingDown))
                return;

            character?.specialAttack3?.Use();
                
            Debug.Log($"[Special Attack 3 Input] Used Special Attack 3!");
        }

        private void OnSpecialAttack2(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.specialAttack2?.isActive) && CheckBoolean(character?.specialAttack2?.isCoolingDown))
                return;

            character?.specialAttack2?.Use();
                
            Debug.Log($"[Special Attack 2 Input] Used Special Attack 2!");
        }

        private void OnSpecialAttack1(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.specialAttack1?.isActive) && CheckBoolean(character?.specialAttack1?.isCoolingDown))
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
            if(CheckBoolean(character?.rangeAttack?.isActive) && CheckBoolean(character?.rangeAttack?.isCoolingDown))
                return;

            character?.rangeAttack?.Use();
                
            Debug.Log($"[Range Attack Input] Used Range Attack!");
        }

        private void OnMeleeAttack(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.meleeAttack?.isActive))
                return;

            character?.meleeAttack?.Use();
                
            Debug.Log($"[Melee Attack Input] Used Melee Attack!");
        }

        private void OnChangeTarget(InputAction.CallbackContext context)
        {
            character?.targetHandler?.GetNextTarget();
                
            Debug.Log($"[Change Target Input] Changed Target");
        }

        // Works directional and then boost (boost and then direction is different action. Only boost is up and down so it won't work yet until boost is actually part of game object)
        private void OnBoost(InputAction.CallbackContext context)
        {
            if(CheckBoolean(character?.boost?.isActive))
                return;

            if(_MovementInput.magnitude < 0.3f)
            {
                float? height = character?.position.y - character?.controller?.flightPlane?.position.y;
                
                if(height < 0.4f) // Temp
                {
                    character?.boost?.VerticalBoost(1);

                    Debug.Log($"[Boost Input] Vertical Boost Up!");
                }

                else if(height > 1) // Temp
                {
                    character?.boost?.VerticalBoost(-1);

                    Debug.Log($"[Boost Input] Vertical Boost Down!");
                }

                else
                    Debug.Log($"[Boost Input] Vertical Boost!");
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

        public override void SetupInput(InputActionAsset inputActionAsset, params InputDevice[] devices)
        {
            base.SetupInput(inputActionAsset, devices);
            
            InputActionMap actionMap = inputActionAsset.FindActionMap("Game", true);

            foreach(InputAction inputAction in actionMap.actions)
                _InputActions.Add(inputAction.name, inputAction);

            _InputActions["Movement"].performed += OnMovement;
            _InputActions["Movement"].canceled += OnMovement;

            _InputActions["Boost"].started += OnBoost;

            _InputActions["ChangeTarget"].started += OnChangeTarget;
            _InputActions["MeleeAttack"].started += OnMeleeAttack;
            _InputActions["Shield"].started += OnShield;
            _InputActions["SpecialAttack1"].started += OnSpecialAttack1;
            _InputActions["SpecialAttack2"].started += OnSpecialAttack2;
            _InputActions["SpecialAttack3"].started += OnSpecialAttack3;

            _InputActionUpdates.Add("RangeAttack", new InputActionUpdate(_InputActions["RangeAttack"], OnRangeAttack));
        }

        public override void EnableInput()
        {
            foreach(InputAction inputAction in _InputActions.Values)
                inputAction.Enable();
        }

        public override void DisableInput()
        {
            foreach(InputAction inputAction in _InputActions.Values)
                inputAction.Disable();
        }

        public override void EnableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Enable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");
        }

        public override void DisableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Disable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");
        }
    }
}