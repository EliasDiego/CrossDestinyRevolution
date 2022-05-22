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

        private InputAction _MovementAction;
        private InputAction _BoostAction;
        private InputAction _ChangeTargetAction;
        private InputAction _MeleeAttackAction;
        private InputAction _RangeAttackAction;
        private InputAction _ShieldAction;
        private InputAction _SpecialAttack1Action;
        private InputAction _SpecialAttack2Action;
        private InputAction _SpecialAttack3Action;

        private void OnSpecialAttack3(InputAction.CallbackContext context)
        {
            character?.specialAttack3?.Use();
                
            Debug.Log($"[Special Attack 3 Input] Used Special Attack 3!");
        }

        private void OnSpecialAttack2(InputAction.CallbackContext context)
        {
            character?.specialAttack2?.Use();
                
            Debug.Log($"[Special Attack 2 Input] Used Special Attack 2!");
        }

        private void OnSpecialAttack1(InputAction.CallbackContext context)
        {
            character?.specialAttack1?.Use();
                
            Debug.Log($"[Special Attack 1 Input] Used Special Attack 1!");
        }

        private void OnShield(InputAction.CallbackContext context)
        {
            character?.shield?.Use();
                
            Debug.Log($"[Shield Input] Used Shield!");
        }

        private void OnRangeAttack(InputAction.CallbackContext context)
        {
            character?.rangeAttack?.Use();
                
            Debug.Log($"[Range Attack Input] Used Range Attack!");
        }

        private void OnMeleeAttack(InputAction.CallbackContext context)
        {
            character?.meleeAttack?.Use();
                
            Debug.Log($"[Melee Attack Input] Used Melee Attack!");
        }

        private void OnChangeTarget(InputAction.CallbackContext context)
        {
            character?.targetHandler?.GetNextTarget();
                
            Debug.Log($"[Change Target Input] Changed Target");
        }

        private void OnBoost(InputAction.CallbackContext context)
        {
            if(character?.boost == null)
                return;

            if(_MovementInput.magnitude < 0.3f)
            {
                float height = character.position.y - character.controller.flightPlane.position.y;
                
                if(height < 0.4f)
                    character?.boost?.VerticalBoost(1);

                else if(height > 1)
                    character?.boost?.VerticalBoost(-1);
            }

            else
                character?.boost?.HorizontalBoost(context.ReadValue<Vector2>());

            Debug.Log($"[Boost Input] Honestly don't expect the logic to work just yet~ {_MovementInput}");
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            _MovementInput = context.ReadValue<Vector2>();

            character?.movement?.Move(_MovementInput);

            Debug.Log($"[Movement Input] {_MovementInput}");
        }

        public override void EnableInput()
        {
            base.EnableInput();

            InputActionMap actionMap = inputActionAsset.FindActionMap("Game", true);

            _MovementAction = actionMap.FindAction("Movement", true);
            _BoostAction = actionMap.FindAction("Boost", true);
            _ChangeTargetAction = actionMap.FindAction("ChangeTarget", true);
            _MeleeAttackAction = actionMap.FindAction("MeleeAttack", true);
            _RangeAttackAction = actionMap.FindAction("RangeAttack", true);
            _ShieldAction = actionMap.FindAction("Shield", true);
            _SpecialAttack1Action = actionMap.FindAction("SpecialAttack1", true);
            _SpecialAttack2Action = actionMap.FindAction("SpecialAttack2", true);
            _SpecialAttack3Action = actionMap.FindAction("SpecialAttack3", true);

            _MovementAction.performed += OnMovement;
            _MovementAction.canceled += OnMovement;

            _BoostAction.Enable();
            _BoostAction.started += OnBoost;
            _BoostAction.performed += OnBoost;
            _BoostAction.canceled += OnBoost;

            _ChangeTargetAction.started += OnChangeTarget;
            _MeleeAttackAction.started += OnMeleeAttack;
            _RangeAttackAction.started += OnRangeAttack;
            _ShieldAction.started += OnShield;
            _SpecialAttack1Action.started += OnSpecialAttack1;
            _SpecialAttack2Action.started += OnSpecialAttack2;
            _SpecialAttack3Action.started += OnSpecialAttack3;
        }
    }
}