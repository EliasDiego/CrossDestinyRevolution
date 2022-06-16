using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    public interface IMenuCancelHandler
    {
        void OnCancel();
    }

    public interface IMenuSubmitHandler
    {
        void OnSubmit();
    }
}