using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusMenu : AnimatedMenu
    {
        [Header("Versus Data")]
        [SerializeField]
        VersusData _VersusData;

        public VersusData versusData => _VersusData;
    }
}