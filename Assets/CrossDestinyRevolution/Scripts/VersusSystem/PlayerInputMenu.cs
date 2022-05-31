using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputMenu : Menu
    {
        [Header("Versus")]
        [SerializeField]
        VersusData _VersusData;
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;
        [SerializeField]
        InputActionAsset _ActionAsset;
        [SerializeField]
        InputActionAsset _SplitKeyboardActionAsset;

        private IPlayerData SetPlayerData(InputActionAsset actionAsset, params InputDevice[] devices)
        {
            PlayerData playerData = new PlayerData();

            playerData.actionAsset = actionAsset;
            playerData.devices = devices;

            return playerData; 
        }

        public override void Show()
        {
            base.Show();
            
            IPlayerData player1Data;
            IPlayerData player2Data;

            if(Gamepad.all.Count > 2)
            {
                _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current, Gamepad.all[0]);
                _Player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.all[1]);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current, Gamepad.all[0]);
                player2Data = SetPlayerData(_ActionAsset, Gamepad.all[1]);
            }

            else if(Gamepad.all.Count == 1)
            {
                _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
                _Player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.current);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
                player2Data = SetPlayerData(_ActionAsset, Gamepad.current);
            }

            else
            {
                return;

                _Player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
                _Player2Input.SetupInput(_SplitKeyboardActionAsset.FindActionMap("UI", true), Keyboard.current);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
                player2Data = SetPlayerData(_SplitKeyboardActionAsset, Keyboard.current);
            }

            _Player1Input.EnableInput();
            _Player2Input.EnableInput();

            _VersusData.player1Data = player1Data;
            _VersusData.player2Data = player2Data;

            string deviceNames = "";

            foreach(InputDevice d in _Player1Input.user.pairedDevices)
                deviceNames += "\n" + d.name;

            Debug.Log("Player 1:" + deviceNames);

            deviceNames = "";

            foreach(InputDevice d in _Player2Input.user.pairedDevices)
                deviceNames += "\n" + d.name;

            Debug.Log("Player 2:" + deviceNames);
        }

        public override void Hide()
        {
            base.Hide();

            _Player1Input.DisableInput();
            _Player2Input.DisableInput();
        }
    }
}