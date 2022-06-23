using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MultipleUsersVersusMenu : VersusMenu
    {
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        InputSystemUIInputModule _InputModule;
        [Header("UI Input")]
        [SerializeField]
        PlayerUIInput[] _PlayerInputs;

        public PlayerUIInput[] playerInputs => _PlayerInputs;
        public EventSystem eventSystem => _EventSystem;
        public InputSystemUIInputModule inputModule => _InputModule;

        public override void Show()
        {
            base.Show();

            _EventSystem?.gameObject.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();

            foreach(PlayerUIInput playerInput in playerInputs.Where(p => p.isAssignedInput))
                playerInput.DisableInput();

            _EventSystem?.gameObject.SetActive(true);
        }
    }
}