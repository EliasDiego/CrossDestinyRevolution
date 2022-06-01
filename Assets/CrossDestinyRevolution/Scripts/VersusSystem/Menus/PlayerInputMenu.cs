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
    public class PlayerInputMenu : VersusMenu
    {
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
                player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current, Gamepad.all[0]);
                player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.all[1]);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current, Gamepad.all[0]);
                player2Data = SetPlayerData(_ActionAsset, Gamepad.all[1]);
            }

            else if(Gamepad.all.Count == 1)
            {
                player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
                player2Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Gamepad.current);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
                player2Data = SetPlayerData(_ActionAsset, Gamepad.current);
            }

            else
            {
                return;

                player1Input.SetupInput(_ActionAsset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
                player2Input.SetupInput(_SplitKeyboardActionAsset.FindActionMap("UI", true), Keyboard.current);

                player1Data = SetPlayerData(_ActionAsset, Keyboard.current, Mouse.current);
                player2Data = SetPlayerData(_SplitKeyboardActionAsset, Keyboard.current);
            }

            player1Input.EnableInput();
            player2Input.EnableInput();

            versusData.player1Data = player1Data;
            versusData.player2Data = player2Data;
        }
    }
}