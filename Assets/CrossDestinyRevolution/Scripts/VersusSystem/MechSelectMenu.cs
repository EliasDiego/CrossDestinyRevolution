using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MechSelectMenu : Menu, IMechSelectMenu
    {
        [Header("Versus Data")]
        [SerializeField]
        VersusData _VersusData;
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput _Player1Input;
        [SerializeField]
        PlayerUIInput _Player2Input;

        public void PickMech(int player, IMechData mechData)
        {
            // switch(player)
            // {
            //     case 1:
            //         _VersusData.player1MechData = mechData;
            //         break;
            //     case 2:
            //         _VersusData.player2MechData = mechData;
            //         break;
            // }
        }

        public void PickMech(MechData mechData)
        {
            
        }

        public override void Show()
        {
            base.Show();

            _VersusData.player1Data.mechData = null;
            _VersusData.player2Data.mechData = null;

            _Player1Input.EnableInput();
            _Player2Input.EnableInput();
        }

        public override void Hide()
        {
            base.Hide();

            _Player1Input.DisableInput();
            _Player2Input.DisableInput();

        }
    }
}