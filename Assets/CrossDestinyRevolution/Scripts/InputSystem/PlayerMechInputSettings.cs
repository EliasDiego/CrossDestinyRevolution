using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    [CreateAssetMenu(menuName = "InputSystem/Settings/Mech/Player Mech Input Settings")]
    public class PlayerMechInputSettings : ScriptableObject, IPlayerMechInputSettings
    {
        [SerializeField]
        BoostInputSettings _BoostInputSettings;

        public IBoostInputSettings boostInputSettings => _BoostInputSettings;
    }
}