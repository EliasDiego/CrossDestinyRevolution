using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MechButton : MonoBehaviour, IPlayerSubmitHandler, IPlayerCancelHandler
    {
        [SerializeField]
        MechData _MechData;
        [SerializeField]
        MechSelectMenu _MechSelectMenu;

        public void OnPlayerCancel(IPlayerInput playerInput)
        {
            Debug.Log("Cancel");
        }

        public void OnPlayerSubmit(IPlayerInput playerInput)
        {
            _MechSelectMenu.PickMech(playerInput, _MechData);
        }
    }
}