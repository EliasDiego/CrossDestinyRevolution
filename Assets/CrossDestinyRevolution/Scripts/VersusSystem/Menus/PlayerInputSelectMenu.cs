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
using CDR.ObjectPoolingSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputSelectMenu : MultipleUsersVersusMenu, IMenuCancelHandler, IObserver<InputEventPtr>
    {
        [Header("UI Input")]
        [SerializeField]
        GameObject _CameraPrefab;
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
        [SerializeField]
        ObjectPooling _Pool;

        List<InputDevice> _DevicesInScreen = new List<InputDevice>();
        List<PlayerUIInput> _ActivePlayerInputs = new List<PlayerUIInput>();

        private IDisposable _Disposable;

        private void Awake() 
        {
            _Pool?.Initialize();
        }

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

            InputActionMap gameActionMap = _ActionAsset.FindActionMap("Game", true);

            _Disposable = UnityEngine.InputSystem.InputSystem.onEvent.Subscribe(this);

            foreach(InputDevice device in UnityEngine.InputSystem.InputSystem.devices.Where(i => gameActionMap.IsUsableWithDevice(i)))
            {
                PlayerDeviceInput deviceInput = _Pool.GetPoolable().GetComponent<PlayerDeviceInput>();

                deviceInput.gameObject.SetActive(true);
                deviceInput.transform.SetParent(transform, false);
                deviceInput.transform.localPosition = Vector3.zero;
                deviceInput.AssignInput(gameActionMap, device);
                deviceInput.EnableInput();
            }

            versusData.participantDataList.Clear();
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
        }

        public void SetPlayerDevice(int playerIndex, params InputDevice[] devices)
        {
            // if(_PlayerDevices.Length < playerIndex + 1)
            // {
            //     Debug.LogError("index is less than what is in capacity");
            // }

            // _PlayerDevices[playerIndex].AddRange(devices.Except(_PlayerDevices[playerIndex]));

            // if(_PlayerDevices.Any(l => l.Count <= 0))
            //     return;

            // for(int i = 0; i < _PlayerDevices.Length; i++)
            // {
            //     versusData.participantDataList.Add(SetPlayerData(_ActionAsset, _PlayerDevices[i].ToArray()));

            //     playerInputs[i].AssignInput(_ActionAsset.FindActionMap("UI"), _PlayerDevices[i].ToArray());
            //     playerInputs[i].DisableInput();
            // }

            // Debug.Log(versusData.participantDatas.Length);

            // SwitchTo(_VersusSettingsMenu);
        }
    }
}