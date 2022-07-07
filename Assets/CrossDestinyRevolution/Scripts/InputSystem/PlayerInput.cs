using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace CDR.InputSystem
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private InputUser _User;
        private InputActionMap _ActionMap;
        private Gamepad[] _Gamepads;

        private bool _IsAssignedInput = false;

        private bool _IsEnabled = false;

        private Dictionary<string, InputActionUpdate> _InputActionUpdates = new Dictionary<string, InputActionUpdate>();
        private Dictionary<string, InputAction> _InputActions = new Dictionary<string, InputAction>();

        private bool _isGamepad = false;

        public event Action<IInput> onEnableInput;
        public event Action<IInput> onDisableInput;
        public event Action<IPlayerInput> onAssignInput;
        public event Action<IPlayerInput> onUnassignInput;

        protected InputActionMap actionMap => _ActionMap;
        
        public InputDevice[] pairedDevices => _User.valid ? _User.pairedDevices.ToArray() : null;
        
        public bool isEnabled => _IsEnabled;

        public bool isAssignedInput => _IsAssignedInput;
        
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

        protected virtual void OnDestroy()
        {
            UnassignInput();
        }

        private bool IsUserNotNull(InputUser user)
        {
            if(user == null)
            {
                Debug.LogAssertion("[Input System Error] Input User has not yet been created!");

                return false;
            }

            return true;
        }

        private bool IsDeviceValid(InputDevice[] devices)
        {
            if(devices == null || devices.Length <= 0)
            {
                Debug.LogAssertion("[Input System Error] No Device(s) Assigned!");

                return false;
            }

            return true;
        }

        protected bool CheckBoolean(bool? boolean)
        {
            return boolean.HasValue && boolean.Value;
        }

        protected void AddInputActionToUpdate(string name, Action action)
        {
            if(GetInputAction(name, out InputAction inputAction))
                AddInputActionToUpdate(name, inputAction, action);
        }

        protected void AddInputActionToUpdate(string name, InputAction inputAction, Action action)
        {
            InputActionUpdate inputActionUpdate = new InputActionUpdate(inputAction, action);

            if(_InputActionUpdates.ContainsKey(name))
                _InputActionUpdates[name] = inputActionUpdate;

            else
                _InputActionUpdates.Add(name, inputActionUpdate);
        }

        protected void RemoveInputActionFromUpdate(string name)
        {
            if(_InputActionUpdates.ContainsKey(name))
                _InputActionUpdates.Remove(name);
        }

        protected bool GetInputAction(string name, out InputAction inputAction)
        {
            if(_InputActions.ContainsKey(name))
            {
                inputAction = _InputActions[name];

                return true;
            }

            Debug.LogWarning($"[Input Error] {name} Input Action doesn't exist!");

            inputAction = null;

            return false;
        }

        protected void StartHaptic(float lowFrequency, float highFrequency)
        {
            if(!_isGamepad)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        protected void StopHaptic()
        {
            if(!_isGamepad)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.ResetHaptics();
        }

        public void PairDevice(params InputDevice[] devices)
        {
            if(!IsUserNotNull(_User) || !IsDeviceValid(devices))
                return;

            _Gamepads = devices?.Where(d => d is Gamepad)?.Cast<Gamepad>()?.ToArray();

            _isGamepad = _Gamepads != null && _Gamepads.Length > 0;
            
            foreach(InputDevice device in devices)
                _User = InputUser.PerformPairingWithDevice(device, _User);
        }

        public void UnpairDevice(params InputDevice[] devices)
        {
            if(!IsUserNotNull(_User) || !_User.valid || !IsDeviceValid(devices))
                return;

            foreach(InputDevice device in devices)
                _User.UnpairDevice(device);
        }

        public virtual void AssociateActionMap(InputActionMap inputActionMap)
        {
            if(!_User.valid)
                return;

            _ActionMap = inputActionMap.Clone();

            _InputActions.Clear();

            foreach(InputAction inputAction in actionMap.actions)
                _InputActions.Add(inputAction.name, inputAction);

            _User.AssociateActionsWithUser(_ActionMap);
        }

        public void AssignInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            if(isAssignedInput)
                UnassignInput();

            _User = default(InputUser);

            devices = devices?.Where(d => d != null)?.ToArray();

            PairDevice(devices);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            AssociateActionMap(inputActionMap);

            _IsAssignedInput = true;
            
            onAssignInput?.Invoke(this);
        }

        public void UnassignInput()
        {
            if(_User != null && _User.valid)
                _User.UnpairDevicesAndRemoveUser();

            _IsAssignedInput = false;
            
            onUnassignInput?.Invoke(this);
        }

        public virtual void EnableInput()
        {
            foreach(InputAction inputAction in _InputActions?.Values?.Where(i => !i.enabled))
                inputAction.Enable();

            _IsEnabled = true;

            onEnableInput?.Invoke(this);
        }

        public virtual void DisableInput()
        {
            foreach(InputAction inputAction in _InputActions?.Values?.Where(i => i.enabled))
                inputAction.Disable();
            
            _IsEnabled = false;

            onDisableInput?.Invoke(this);
        }

        public void EnableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Enable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;

            onEnableInput?.Invoke(this);
        }

        public void DisableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Disable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;

            onDisableInput?.Invoke(this);
        }

        public void EnableInputExcept(params string[] except)
        {
            foreach(InputAction inputAction in _InputActions?.Values?.Where(i => !except.Contains(i.name)))
                inputAction.Enable();

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;

            onEnableInput?.Invoke(this);
        }

        public void DisableInputExcept(params string[] except)
        {
            foreach(InputAction inputAction in _InputActions?.Values?.Where(i => !except.Contains(i.name)))
                inputAction.Disable();

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;

            onDisableInput?.Invoke(this);
        }
    }
}