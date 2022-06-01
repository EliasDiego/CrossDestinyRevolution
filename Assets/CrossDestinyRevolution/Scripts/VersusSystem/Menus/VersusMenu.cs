using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusMenu : Menu
    {
        [Header("Versus Data")]
        [SerializeField]
        VersusData _VersusData;
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;

        public PlayerUIInput player1Input => _Player1Input;
        public PlayerUIInput player2Input => _Player2Input;

        public VersusData versusData => _VersusData;

        public override void Hide()
        {
            base.Hide();

            _Player1Input.DisableInput();
            _Player2Input.DisableInput();
        }
    }
}