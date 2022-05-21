using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            IPlayerInput playerInput = InputUtilities.AssignPlayerInput<PlayerMechInput>(_TestObject, _InputActionAsset, Keyboard.current);

            playerInput.EnableInput();
        }
    }
}