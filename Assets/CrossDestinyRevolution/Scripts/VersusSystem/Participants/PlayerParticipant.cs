using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class PlayerParticipant : Participant, ICameraParticipant
    {
        IPlayerMechBattleUI _BattleUI;
        Camera _Camera;

        public Rect cameraRect { set { if(_Camera) _Camera.rect = value; } }

        public PlayerParticipant(IMech mech, IPlayerMechBattleUI battleUI, Camera camera, Vector3 startPosition, Quaternion startRotation) : base(mech, startPosition, startRotation)
        {
            _BattleUI = battleUI;
            _Camera = camera;
        }

        public override void Start()
        {
            Reset();

            base.Start();

            _BattleUI.SetMech(mech);
            _BattleUI.Show();
        }
    }
}