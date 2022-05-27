using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

namespace CDR.VersusSystem
{
    public class PlayerData : IPlayerData
    {
        public InputUser user { get; set; }
        public IMechData mechData { get; set; }
    }
}