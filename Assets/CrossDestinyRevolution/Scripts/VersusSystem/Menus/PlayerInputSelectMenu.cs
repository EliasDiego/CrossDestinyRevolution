using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputSelectMenu : MultipleUsersVersusMenu, IObserver<InputEventPtr>
    {
        [Header("Versus Stuff")]
        [SerializeField]
        private GameObject _CameraPrefab;
        [SerializeField]
        private InputActionAsset _ActionAsset;
        [SerializeField]
        private InputActionAsset _SplitKeyboardActionAsset;
        [SerializeField]
        private PlayerMechInputSettings _Settings;
        [SerializeField]
        private GameObject _BattleUIPrefab;
        
        [Header("Navigation")]
        [SerializeField]
        private VersusSettingsMenu _VersusSettingsMenu;

        [Header("Player Select")]
        [SerializeField]
        private PlayerIndexImageHandler[] _PlayerIndexImageHandlers;

        [SerializeField]
        LayerMask _Layermask;

        private int _CurrentPlayerIndex = 0;
        private InputDevice _CurrentDevice;
        private InputActionAsset _CurrentInputActionAsset;
        private List<InputDevice> _PlayerDevices = new List<InputDevice>();

        private IDisposable _Disposable;

        private PlayerUIInput _PlayerSelectInput;

        private void Awake() 
        {
            _PlayerSelectInput = GetComponent<PlayerUIInput>();
        }
        
        private IParticipantData SetPlayerData(InputActionAsset actionAsset, params InputDevice[] devices)
        {
            return new PlayerParticipantData() 
            { 
                settings = _Settings,
                cameraPrefab = _CameraPrefab,
                actionAsset = actionAsset, 
                devices = devices, 
                battleUIPrefab = _BattleUIPrefab 
            }; 
        }

        private void ResetImageHandlers()
        {
            foreach(PlayerIndexImageHandler handler in _PlayerIndexImageHandlers)
                handler.Hide();
        }

        private void UpdateImageHandlers(int currentIndex)
        {
            if(currentIndex >= _PlayerIndexImageHandlers.Length)
                return;

            if(currentIndex > 0)
                _PlayerIndexImageHandlers[currentIndex - 1].Hide();

            _PlayerIndexImageHandlers[currentIndex].Show();
        }

        private void OnStart(InputAction.CallbackContext context)
        {
            versusData.participantDataList.Add(SetPlayerData(_CurrentInputActionAsset, _CurrentDevice));

            if(versusData.participantDataList.Count >= 2)
                SwitchTo(_VersusSettingsMenu);
            else
                _CurrentPlayerIndex++;
            
            UpdateImageHandlers(_CurrentPlayerIndex);

            _PlayerDevices.Add(_CurrentDevice);

            _PlayerSelectInput.UnassignInput();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(previousMenu != null)
                Back();
        }

        public override void Show()
        {
            base.Show();

            _Disposable = UnityEngine.InputSystem.InputSystem.onEvent.Subscribe(this);

            _CurrentPlayerIndex = 0;
            
            versusData.participantDataList.Clear();

            _PlayerDevices.Clear();

            ResetImageHandlers();

            UpdateImageHandlers(_CurrentPlayerIndex);
        }

        public override void Hide()
        {
            base.Hide();
            
            _Disposable.Dispose();

            _PlayerSelectInput.DisableInput();
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(InputEventPtr value)
        {
            try
            {
                if(value.GetFirstButtonPressOrNull() == null)
                    return;
            }

            catch
            {
                return;
            }

            InputDevice device = UnityEngine.InputSystem.InputSystem.GetDeviceById(value.deviceId);
            
            if(device == null)
                return;

            InputActionMap gameActionMap = _ActionAsset.FindActionMap("Game", true);

            if(!gameActionMap.IsUsableWithDevice(device))
                return;

            if(device is Keyboard)
                _CurrentInputActionAsset = _PlayerDevices.Any(d => d is Keyboard) ? _SplitKeyboardActionAsset : _ActionAsset;

            else if(_PlayerDevices.Contains(device))
                return;

            _PlayerSelectInput.AssignInput(_CurrentInputActionAsset.FindActionMap("UI", true), device);

            _CurrentDevice = device;
            
            _PlayerSelectInput.EnableInput();
            _PlayerSelectInput.onStart += OnStart;
            _PlayerSelectInput.onCancel += OnCancel;
        }
    }
}