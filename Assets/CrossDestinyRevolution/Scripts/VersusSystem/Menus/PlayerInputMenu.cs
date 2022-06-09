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
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;
        [SerializeField]
        InputActionAsset _ActionAsset;
        [SerializeField]
        InputActionAsset _SplitKeyboardActionAsset;
        [SerializeField]
        GameObject _BattleUIPrefab;

        private IParticipantData SetPlayerData(InputActionAsset actionAsset, params InputDevice[] devices)
        {
            return new PlayerParticipantData() 
            { 
                actionAsset = actionAsset, 
                devices = devices, 
                battleUIPrefab = _BattleUIPrefab 
            }; 
        }

        public override void Show()
        {
            base.Show();
            
            IParticipantData player1Data;
            IParticipantData player2Data;

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

            versusData.player1Data = player1Data;
            versusData.player2Data = player2Data;
        }
    }
}