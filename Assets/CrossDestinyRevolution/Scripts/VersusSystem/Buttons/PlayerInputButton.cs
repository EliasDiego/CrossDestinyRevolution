using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputButton : MonoBehaviour, IPlayerSubmitHandler
    {
        [SerializeField]
        int _PlayerIndex;
        [SerializeField]
        PlayerInputSelectMenu _Menu;

        public void OnPlayerSubmit(IPlayerInput playerInput)
        {
            Debug.Log(playerInput);
            _Menu.SetPlayerDevice(_PlayerIndex, playerInput.pairedDevices);
        }
    }
}