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
        
        public Camera camera => _Camera;

        public PlayerParticipant(string name, IMech mech, IPlayerMechBattleUI battleUI, Camera camera, Vector3 startPosition, Quaternion startRotation) : base(name, mech, startPosition, startRotation)
        {
            _BattleUI = battleUI;
            _Camera = camera;

            _BattleUI.camera = camera;
        }

        public override void Start()
        {
            mech.health.ModifyValue(mech.health.MaxValue);
            mech.boost?.boostValue.ModifyValue(mech.boost.boostValue.MaxValue);

            base.Start();

            _BattleUI.SetMech(mech);
            _BattleUI.Show();
        }
    }
}