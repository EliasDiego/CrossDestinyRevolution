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
    public class PlayerInputSelectMenu : VersusMenu, IMenuCancelHandler, IObserver<InputEventPtr>
    {
        [Header("UI Input")]
        [SerializeField]
        GameObject _CameraPrefab;
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;
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

        [Header("Player Input Select")]
        [SerializeField]
        Image _Player1Image;
        [SerializeField]
        Image _Player2Image;

        private IDisposable _Disposable;

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

            // _SplitKeyboardActionAsset.actionMaps[0].devices.actions[0].controls

            // InputUser a;

            // a.AssociateActionsWithUser()
            
            // IParticipantData player1Data;
            // IParticipantData player2Data;

            // if(Gamepad.all.Count > 2)
            // {
            //     _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current, Gamepad.all[0]);
            //     _Player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.all[1]);

            //     player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current, Gamepad.all[0]);
            //     player2Data = SetPlayerData(_ActionAsset, Gamepad.all[1]);
            // }

            // else if(Gamepad.all.Count == 1)
            // {
            //     _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
            //     _Player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.current);

            //     player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
            //     player2Data = SetPlayerData(_ActionAsset, Gamepad.current);
            // }

            // else
            // {
            //     return;

            //     _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
            //     _Player2Input.SetupInput(_SplitKeyboardActionAsset.FindActionMap("UI", true), Keyboard.current);

            //     player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
            //     player2Data = SetPlayerData(_SplitKeyboardActionAsset, Keyboard.current);
            // }
            
            // versusData.player1Data = player1Data;
            // versusData.player2Data = player2Data;
        }

        public override void Hide()
        {
            base.Hide();

            if(_Disposable != null)
                _Disposable.Dispose();
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

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

        public void SetPlayerDevice(int playerIndex, InputDevice device)
        {
            
        }
    }
}