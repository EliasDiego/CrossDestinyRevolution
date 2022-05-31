using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace CDR.VersusSystem
{
    public class PlayerData : IPlayerData
    {
        public InputDevice[] devices { get; set; }
        public InputActionAsset actionAsset { get; set; }
        public IMechData mechData { get; set; }
    }
}