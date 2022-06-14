using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MultipleUsersVersusMenu : VersusMenu
    {
        [SerializeField]
        GameObject _EventSystemObject;
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput[] _PlayerInputs;

        public PlayerUIInput[] playerInputs => _PlayerInputs;

        public override void Show()
        {
            base.Show();

            _EventSystemObject?.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();

            foreach(PlayerUIInput playerInput in playerInputs)
                playerInput.DisableInput();

            _EventSystemObject?.SetActive(true);
        }
    }
}