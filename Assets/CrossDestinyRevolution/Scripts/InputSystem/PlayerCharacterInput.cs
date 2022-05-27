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
    public abstract class PlayerCharacterInput<TCharacter> : CharacterInput<TCharacter>, IPlayerInput 
        where TCharacter : IActiveCharacter
    {
        private InputUser _User;
        private InputActionMap _ActionMap;
        private Gamepad[] _Gamepads;

        protected InputActionMap actionMap => _ActionMap;
        
        public InputUser user => _User;

        protected virtual void OnDestroy()
        {
            if(user != null && user.valid)
                user.UnpairDevicesAndRemoveUser();
        }

        protected void StartHaptic(float lowFrequency, float highFrequency)
        {
            if(_Gamepads == null || _Gamepads.Length <= 0)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        protected void StopHaptic()
        {
            if(_Gamepads == null || _Gamepads.Length <= 0)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.ResetHaptics();
        }

        public virtual void SetupInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            _User = default(InputUser);

            devices = devices.Where(d => d != null)?.ToArray();

            if(devices == null || devices.Length <= 0)
            {
                Debug.LogAssertion("[Input System Error] No Device(s) Assigned!");

                return;
            }

            _Gamepads = devices.Where(d => d is Gamepad)?.Cast<Gamepad>()?.ToArray();
            
            foreach(InputDevice device in devices)
                _User = InputUser.PerformPairingWithDevice(device, _User);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            _ActionMap = inputActionMap.Clone();

            _User.AssociateActionsWithUser(_ActionMap);
        }

        public override void EnableInput()
        {
            actionMap.Enable();
        }

        public override void DisableInput()
        {
            actionMap.Disable();
        }

        public abstract void EnableInput(string name);

        public abstract void DisableInput(string name);
    }
}