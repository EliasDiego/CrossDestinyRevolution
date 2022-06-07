using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MultipleUsersVersusMenu : VersusMenu
    {
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;

        public PlayerUIInput player1Input => _Player1Input;
        public PlayerUIInput player2Input => _Player2Input;

        public override void Hide()
        {
            base.Hide();

            player1Input.DisableInput();
            player2Input.DisableInput();
        }
    }
}