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
    public class PlayerInputSelectMenu : MultipleUsersVersusMenu, IMenuCancelHandler, IObserver<InputEventPtr>
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

        private int _CurrentPlayerIndex = 0;
        private InputDevice _CurrentDevice;
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
            versusData.participantDataList.Add(SetPlayerData(_ActionAsset, _CurrentDevice));

            if(versusData.participantDataList.Count >= 2)
                SwitchTo(_VersusSettingsMenu);
            else
                _CurrentPlayerIndex++;
            
            UpdateImageHandlers(_CurrentPlayerIndex);
        }

        public void OnCancel()
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

            if(_CurrentDevice == device || !gameActionMap.IsUsableWithDevice(device) || _PlayerDevices.Contains(device))
                return;

            _CurrentDevice = device;
                
            _PlayerSelectInput.AssignInput(_ActionAsset.FindActionMap("UI", true), device);
            _PlayerSelectInput.EnableInput();
            _PlayerSelectInput.onStart += OnStart;
        }
    }
}