using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MechButton : MonoBehaviour, IPlayerSubmitHandler
    {
        [SerializeField]
        MechData _MechData;
        [SerializeField]
        MechSelectMenu _MechSelectMenu;

        public void OnPlayerSubmit(IPlayerInput playerInput)
        {
            _MechSelectMenu.PickMech(playerInput, _MechData);
        }
    }
}