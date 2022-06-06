using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace CDR.InputSystem
{
    public class TestUIInputManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _TestObject;
        [SerializeField]
        GameObject _TestObject2;
        [SerializeField]
        InputActionAsset _Asset;

        private void Start()
        {
            PlayerUIInput player1Input = InputUtilities.AssignPlayerInput<PlayerUIInput>(_TestObject, _Asset.FindActionMap("UI", true), Keyboard.current, Mouse.current);
            PlayerUIInput player2Input = InputUtilities.AssignPlayerInput<PlayerUIInput>(_TestObject2, _Asset.FindActionMap("UI", true), Gamepad.current);
            
            player1Input.gameObject.AddComponent<PlayerUIEventHandler>();
            player2Input.gameObject.AddComponent<PlayerUIEventHandler>();

            player1Input.EnableInput();
            player2Input.EnableInput();
        }

        public void Test() 
        {
            Debug.Log("Test");
        }
    }
}