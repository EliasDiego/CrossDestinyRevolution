using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;

namespace CDR.Prototype_SR
{
    public class VersusManager : MonoBehaviour
    {
        [Header("Mech")]
        [SerializeField]
        Mech _Player1Mech;
        [SerializeField]
        Mech _Player2Mech;

        [Header("Input Action Asset")]
        [SerializeField]
        InputActionAsset _MainInputAsset;
        [SerializeField]
        InputActionAsset _SplitInputAsset;

        [Header("Player Input Settings")]
        [SerializeField]
        PlayerMechInputSettings _PlayerMechInputSettings;

        private void Start() 
        {
            PlayerMechInput player1Input;
            PlayerMechInput player2Input;

            if(Gamepad.all.Count > 1)
            {
                player1Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset.FindActionMap("Game", true), Gamepad.all[0]);
                player2Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _MainInputAsset.FindActionMap("Game", true), Gamepad.all[1]);
                Debug.Log("Blah>1");
            }

            else if(Gamepad.all.Count == 1)
            {
                player1Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset.FindActionMap("Game", true), Keyboard.current);
                player2Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _MainInputAsset.FindActionMap("Game", true), Gamepad.all[0]);
                Debug.Log("Blah==1");
            }

            else
            {
                player1Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset.FindActionMap("Game", true), Keyboard.current);
                player2Input = InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _SplitInputAsset.FindActionMap("Game", true), Keyboard.current);
                Debug.Log("Blah==0");
            }

            player1Input.settings = _PlayerMechInputSettings;
            player2Input.settings = _PlayerMechInputSettings;

            Debug.Log("Player 1");
            foreach(InputDevice device in player1Input.user.pairedDevices)
                Debug.Log(device.name);

            Debug.Log("Player 2");
            foreach(InputDevice device in player2Input.user.pairedDevices)
                Debug.Log(device.name);

            player1Input.EnableInput(); 
            player2Input.EnableInput(); 

            _Player1Mech?.targetHandler?.Use();
            _Player2Mech?.targetHandler?.Use();

            _Player1Mech?.movement?.Use();
            _Player2Mech?.movement?.Use();
        }
    }
}