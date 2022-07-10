using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;

using CDR.UISystem;
using CDR.InputSystem;
using CDR.AudioSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputSelectMenu : MultipleUsersVersusMenu, IPlayerInputSelectMenu
    {
        [SerializeField]
        private Transition _MainMenuTransition;
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
        AudioClipPreset _OnPlayerSubmitSFX;
        [SerializeField]
        AudioClipPreset _OnPlayerCancelSFX;
        [SerializeField]
        AudioSource _AudioSource;

        private int _CurrentPlayerIndex = 0;
        private PlayerInputData _CurrentPlayerInputData;
        private List<PlayerInputData> _PlayerInputDatas = new List<PlayerInputData>();

        private IDisposable _Disposable;

        private PlayerUIInput _PlayerSelectInput;
        
        private struct PlayerInputData
        {
            public InputDevice[] devices { get; }
            public InputActionAsset actionAsset { get; }

            public PlayerInputData(InputActionAsset inputActionAsset, params InputDevice[] inputDevices)
            {
                devices = inputDevices;
                actionAsset = inputActionAsset;
            }
        }

        protected override void Awake() 
        {
            base.Awake();
            
            _PlayerSelectInput = GetComponent<PlayerUIInput>();
        }

        private void OnDestroy() 
        {
            if(_Disposable != null)
                _Disposable.Dispose();    
        }
        
        private IParticipantData SetPlayerData(string name, InputActionAsset actionAsset, params InputDevice[] devices)
        {
            return new PlayerParticipantData() 
            { 
                name = name,
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

            for(int i = 0; i < _PlayerIndexImageHandlers.Length; i++)
            {
                if(currentIndex == i)
                    _PlayerIndexImageHandlers[i].Show();

                else
                    _PlayerIndexImageHandlers[i].Hide();
            }
        }

        private void OnStart(InputAction.CallbackContext context)
        {
            versusData.participantDataList.Add(SetPlayerData($"Player {_CurrentPlayerIndex + 1}", _CurrentPlayerInputData.actionAsset, _CurrentPlayerInputData.devices));

            if(_AudioSource)
                _OnPlayerSubmitSFX?.PlayOneShot(_AudioSource);

            if(_CurrentPlayerIndex < _PlayerInputDatas.Count)
                _PlayerInputDatas[_CurrentPlayerIndex] = _CurrentPlayerInputData;

            else
                _PlayerInputDatas.Add(_CurrentPlayerInputData);

            if(versusData.participantDataList.Count >= 2)
            {
                OnPlayerInputsComplete();

                return;
            }
            else
                _CurrentPlayerIndex++;
            
            UpdateImageHandlers(_CurrentPlayerIndex);

            _PlayerSelectInput.UnassignInput();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(_CurrentPlayerIndex > 0)
            {
                _CurrentPlayerIndex--;
            
                UpdateImageHandlers(_CurrentPlayerIndex);

                _PlayerSelectInput.UnassignInput();

                versusData.participantDataList.RemoveAt(_CurrentPlayerIndex);

                _PlayerInputDatas.RemoveAt(_CurrentPlayerIndex);
                    
                if(_AudioSource)
                    _OnPlayerCancelSFX?.PlayOneShot(_AudioSource);

                return;
            }

            _MainMenuTransition?.Back();
        }

        private void OnPlayerInputsComplete()
        {
            for(int i = 0; i < _PlayerInputDatas.Count; i++)
            {
                if(i >= playerInputs.Length)
                    break;

                playerInputs[i].AssignInput(_PlayerInputDatas[i].actionAsset.FindActionMap("UI", true), _PlayerInputDatas[i].devices);
            }

            SwitchTo(_VersusSettingsMenu);
        }

        public override void Show()
        {
            base.Show();

            _Disposable = UnityEngine.InputSystem.InputSystem.onEvent.Subscribe(this);

            _CurrentPlayerIndex = 0;
            
            versusData.participantDataList.Clear();

            _PlayerInputDatas.Clear();

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

            InputActionAsset asset = _ActionAsset;
            
            if(device == null)
                return;
                
            if(!_ActionAsset.FindActionMap("Game", true).IsUsableWithDevice(device))
                return;

            if(device is Keyboard && _PlayerInputDatas.Any(p => p.devices.Any(d => d is Keyboard)))
                asset = _SplitKeyboardActionAsset;

            else if(_PlayerInputDatas.Any(p => p.devices.Contains(device)))
                return;

            _CurrentPlayerInputData = new PlayerInputData(asset, device);

            _PlayerSelectInput.AssignInput(asset.FindActionMap("UI", true), device);
            _PlayerSelectInput.EnableInput();
            _PlayerSelectInput.onStart += OnStart;
            _PlayerSelectInput.onCancel += OnCancel;
        }
    }
}