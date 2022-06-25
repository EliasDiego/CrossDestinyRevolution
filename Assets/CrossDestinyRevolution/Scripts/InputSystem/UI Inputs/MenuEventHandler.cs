using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

using CDR.UISystem;

namespace CDR.InputSystem
{
    public class MenuEventHandler : MonoBehaviour
    {
        InputSystemUIInputModule _InputModule;

        private void Awake() 
        {
            _InputModule = GetComponent<InputSystemUIInputModule>();
            
            if(!_InputModule)
                return;

            _InputModule.cancel.action.started += OnCancel;
            _InputModule.submit.action.started += OnSubmit;
        }

        private void OnEnable() 
        {
            if(!_InputModule)
                return;

            _InputModule.cancel.action.started += OnCancel;
            _InputModule.submit.action.started += OnSubmit;
        }

        private void OnDestroy() 
        {
            if(!_InputModule)
                return;

            _InputModule.cancel.action.started -= OnCancel;
            _InputModule.submit.action.started -= OnSubmit;
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            Menu currentMenu = Menu.menus.FirstOrDefault(m => m.isShown);

            if(currentMenu is IMenuCancelHandler c)
                c.OnCancel();
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            Menu currentMenu = Menu.menus.FirstOrDefault(m => m.isShown);

            if(currentMenu is IMenuSubmitHandler c)
                c.OnSubmit();
        }
    }
}