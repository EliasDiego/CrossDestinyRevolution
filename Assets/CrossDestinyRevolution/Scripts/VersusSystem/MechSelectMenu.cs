using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class MechSelectMenu : Menu, IMechSelectMenu
    {
        [Header("Versus Data")]
        [SerializeField]
        VersusData _VersusData;

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
    }
}