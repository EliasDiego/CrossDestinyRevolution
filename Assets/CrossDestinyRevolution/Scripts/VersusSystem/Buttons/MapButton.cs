using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MapButton : MonoBehaviour, IPlayerSubmitHandler
    {
        [SerializeField]
        MapSelectMenu _MapSelectMenu;
        [SerializeField]
        MapData _MapData;

        public void OnPlayerSubmit(IPlayerInput playerInput)
        {
            _MapSelectMenu.PickMap(playerInput, _MapData);
        }
    }
}