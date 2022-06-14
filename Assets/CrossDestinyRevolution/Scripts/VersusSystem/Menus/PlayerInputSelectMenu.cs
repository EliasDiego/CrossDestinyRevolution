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
        [Header("UI Input")]
        [SerializeField]
        GameObject _CameraPrefab;
        [SerializeField]
        PlayerUIInput _PlayerSelectInput;
        [SerializeField]
        InputActionAsset _ActionAsset;
        [SerializeField]
        InputActionAsset _SplitKeyboardActionAsset;
        [SerializeField]
        PlayerMechInputSettings _Settings;
        [SerializeField]
        GameObject _BattleUIPrefab;
        [Header("UI Input")]
        [SerializeField]
        VersusSettingsMenu _VersusSettingsMenu;

        List<InputDevice>[] _PlayerDevices = new List<InputDevice>[2];
        
        private IDisposable _Disposable;

        private void OnDestroy() 
        {
            if(_Disposable != null)
                _Disposable.Dispose();
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

        public void OnCancel()
        {
            if(previousMenu != null)
                Back();
        }

        public override void Show()
        {
            base.Show();

            _Disposable = UnityEngine.InputSystem.InputSystem.onEvent.Subscribe(this);

            versusData.participantDataList.Clear();

            Debug.Log(versusData.participantDatas.Length);

            for(int i = 0 ; i < _PlayerDevices.Length; i++)
            {
                if(_PlayerDevices[i] == null)
                    _PlayerDevices[i] = new List<InputDevice>();

                else
                    _PlayerDevices[i]?.Clear();
            }
        }

        public override void Hide()
        {
            base.Hide();

            if(_Disposable != null)
                _Disposable.Dispose();
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

            if(_PlayerSelectInput.pairedDevices != null && _PlayerSelectInput.pairedDevices.Contains(device))
                return;

            if(_ActionAsset.FindActionMap("Game").IsUsableWithDevice(device))
            {
                _PlayerSelectInput.SetupInput(_ActionAsset.FindActionMap("UI"), device);
                _PlayerSelectInput.EnableInput();
            }
        }

        public void SetPlayerDevice(int playerIndex, params InputDevice[] devices)
        {
            if(_PlayerDevices.Length < playerIndex + 1)
            {
                Debug.LogError("index is less than what is in capacity");
            }

            _PlayerDevices[playerIndex].AddRange(devices.Except(_PlayerDevices[playerIndex]));

            if(_PlayerDevices.Any(l => l.Count <= 0))
                return;

            for(int i = 0; i < _PlayerDevices.Length; i++)
            {
                versusData.participantDataList.Add(SetPlayerData(_ActionAsset, _PlayerDevices[i].ToArray()));

                playerInputs[i].SetupInput(_ActionAsset.FindActionMap("UI"), _PlayerDevices[i].ToArray());
                playerInputs[i].DisableInput();
            }

            Debug.Log(versusData.participantDatas.Length);

            SwitchTo(_VersusSettingsMenu);
        }
    }
}