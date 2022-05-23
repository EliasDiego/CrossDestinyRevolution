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

        private void Awake() 
        {
            IPlayerInput playerInput = InputUtilities.AssignPlayerInput<PlayerMechInput>(_TestObject, _InputActionAsset, Keyboard.current, Gamepad.current);

            playerInput.EnableInput();

            if(_TestObject.TryGetComponent<ActiveCharacter>(out ActiveCharacter activeCharacter))
                activeCharacter?.targetHandler?.Use();

            if(_TestObject.TryGetComponent<Mech>(out Mech mech))
                mech.movement?.Use();
        }
    }
}