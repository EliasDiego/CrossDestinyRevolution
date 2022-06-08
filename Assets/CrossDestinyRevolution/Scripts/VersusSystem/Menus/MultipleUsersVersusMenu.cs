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
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;

        public PlayerUIInput player1Input => _Player1Input;
        public PlayerUIInput player2Input => _Player2Input;

        public override void Show()
        {
            base.Show();

            _EventSystemObject?.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();

            player1Input.DisableInput();
            player2Input.DisableInput();

            _EventSystemObject?.SetActive(true);
        }
    }
}