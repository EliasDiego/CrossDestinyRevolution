using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public class TestManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _TestObject;
        [SerializeField]
        InputActionAsset _InputActionAsset;
        [SerializeField]
        PlayerMechInputSettings _PlayerMechInputSettings;

        private void Start() 
        {
            PlayerMechInput playerInput = InputUtilities.AssignPlayerInput<PlayerMechInput>(_TestObject, _InputActionAsset.FindActionMap("Game", true), Keyboard.current, Gamepad.current);

            playerInput.settings = _PlayerMechInputSettings;

            playerInput.EnableInput();

            if(_TestObject.TryGetComponent<ActiveCharacter>(out ActiveCharacter activeCharacter))
                activeCharacter?.targetHandler?.Use();

            if(_TestObject.TryGetComponent<Mech>(out Mech mech))
                mech.movement?.Use();
        }
    }
}