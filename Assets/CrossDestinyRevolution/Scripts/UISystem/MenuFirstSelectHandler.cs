using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    public class MenuFirstSelectHandler : MonoBehaviour
    {
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;

        Menu _Menu;

        private void Awake() 
        {
            _Menu = GetComponent<Menu>();

            if(!_EventSystem)
                _EventSystem = EventSystem.current;

            if(_Menu)
                _Menu.onShow += OnEnableMenu;
        }

        private void OnEnableMenu()
        {
            if(!_EventSystem)
                return;

            _EventSystem.SetSelectedGameObject(_FirstSelect);
        }
    }
}