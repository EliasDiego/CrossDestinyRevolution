using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.UISystem;

namespace CDR.InputSystem
{
    public class PlayerMenuEventHandler : MonoBehaviour
    {
        PlayerUIInput _PlayerInput;

        private void Awake() 
        {
            _PlayerInput = GetComponent<PlayerUIInput>();

            _PlayerInput.onAssignInput += OnAssignInput;
        }

        void OnAssignInput(IPlayerInput playerInput)
        {
            PlayerUIInput playerUIInput = playerInput as PlayerUIInput;

            playerUIInput.onCancel += OnCancel;
            playerUIInput.onSubmit += OnSubmit;
        }

        void OnCancel(InputAction.CallbackContext context)
        {
            Menu shownMenu = Menu.menus.FirstOrDefault(m => m.isShown);

            if(shownMenu is IPlayerCancelHandler c)
                c.OnPlayerCancel(_PlayerInput);
        }

        void OnSubmit(InputAction.CallbackContext context)
        {
            Menu shownMenu = Menu.menus.FirstOrDefault(m => m.isShown);

            if(shownMenu is IPlayerSubmitHandler s)
                s.OnPlayerSubmit(_PlayerInput);
        }
    }
}