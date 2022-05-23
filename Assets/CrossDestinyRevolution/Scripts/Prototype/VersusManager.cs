using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;

namespace CDR.Prototype_SC
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

        private void Start() 
        {
            if(Gamepad.all.Count > 1)
            {
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset, Gamepad.all[0]);
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _MainInputAsset, Gamepad.all[1]);
            }

            else if(Gamepad.all.Count == 1)
            {
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset, Keyboard.current);
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _MainInputAsset, Gamepad.all[1]);
            }

            else
            {
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player1Mech.gameObject, _MainInputAsset, Keyboard.current);
                InputUtilities.AssignPlayerInput<PlayerMechInput>(_Player2Mech.gameObject, _SplitInputAsset, Keyboard.current);
            }
        }
    }
}