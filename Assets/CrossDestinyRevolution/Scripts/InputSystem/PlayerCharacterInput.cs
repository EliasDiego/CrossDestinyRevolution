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
    public abstract class PlayerCharacterInput<TCharacter, TSettings> : CharacterInput<TCharacter>, IPlayerInput<TSettings> 
        where TCharacter : IActiveCharacter
        where TSettings : IPlayerInputSettings
    {
        private InputUser _User;
        private InputActionAsset _ActionAsset;
        private TSettings _PlayerInputSettings;

        private Gamepad[] _Gamepads;

        protected InputActionAsset actionAsset => _ActionAsset;
        protected TSettings settings => _PlayerInputSettings;

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

        public virtual void SetupInput(TSettings playerInputSettings, InputActionAsset inputActionAsset, params InputDevice[] devices)
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

            _ActionAsset = Instantiate(inputActionAsset);

            _PlayerInputSettings = playerInputSettings;

            _User.AssociateActionsWithUser(_ActionAsset);
        }

        public override void EnableInput()
        {
            if(actionAsset)
                actionAsset.Enable();
        }

        public override void DisableInput()
        {
            if(actionAsset)
                actionAsset.Disable();
        }

        public abstract void EnableInput(string name);

        public abstract void DisableInput(string name);
    }
}