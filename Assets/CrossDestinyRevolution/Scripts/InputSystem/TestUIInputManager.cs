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

            player1Input.gameObject.AddComponent<UIHandler>();
            player2Input.gameObject.AddComponent<UIHandler>();
            
            player1Input.EnableInput();
            player2Input.EnableInput();

            // UInput.PlayerInput p1 =  UInput.PlayerInput.Instantiate(_TestObject, 1, null, 1, UInput.Keyboard.current);
            // UInput.PlayerInput p2 = UInput.PlayerInput.Instantiate(_TestObject, 2, null, 2, UInput.Gamepad.current);

            // p1.uiInputModule = _TestObject.GetComponent<UInput.UI.InputSystemUIInputModule>();
            // p2.uiInputModule = _TestObject2.GetComponent<UInput.UI.InputSystemUIInputModule>();
        }

        public void Test() 
        {
            Debug.Log("Test");
        }
    }
}