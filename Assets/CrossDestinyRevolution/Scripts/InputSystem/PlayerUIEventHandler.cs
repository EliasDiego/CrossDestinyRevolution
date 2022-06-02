using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.UISystem;

namespace CDR.InputSystem
{
    public class PlayerUIEventHandler : MonoBehaviour
    {
        PlayerUIInput _PlayerUIInput;

        // private void Awake()
        // {
        //     _PlayerUIInput = GetComponent<PlayerUIInput>();

        //     _PlayerUIInput.onSubmit += OnSubmit;
        //     _PlayerUIInput.onCancel += OnCancel;
        // }

        // private void OnSubmit(Selectable selectable)
        // {
        //     if (selectable.TryGetComponent<IPlayerSubmitHandler>(out IPlayerSubmitHandler s))
        //         s.OnPlayerSubmit(_PlayerUIInput);

        //     Menu shownMenu = Menu.menus.FirstOrDefault(m => m.isShown);

        //     if(shownMenu && shownMenu is IPlayerSubmitHandler mS)
        //         mS.OnPlayerSubmit(_PlayerUIInput);    
        // }

        // private void OnCancel(Selectable selectable)
        // {
        //     if(selectable.TryGetComponent<IPlayerCancelHandler>(out IPlayerCancelHandler c))
        //         c.OnPlayerCancel(_PlayerUIInput);

        //     Menu shownMenu = Menu.menus.FirstOrDefault(m => m.isShown);

        //     if(shownMenu && shownMenu is IPlayerCancelHandler mC)
        //         mC.OnPlayerCancel(_PlayerUIInput);    
        // }
    }
}