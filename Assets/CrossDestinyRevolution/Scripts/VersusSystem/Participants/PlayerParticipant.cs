using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;
using CDR.CameraSystem;

namespace CDR.VersusSystem
{
    public class PlayerParticipant : Participant, ICameraParticipant
    {
        private IPlayerMechBattleUI _BattleUI;
        private Camera _Camera;
        private CameraPivot _CameraPivot;
        private Vector3 _CameraPivotStartPosition;
        
        public Camera camera => _Camera;
        public IPlayerMechBattleUI battleUI => _BattleUI;

        public PlayerParticipant(string name, IMech mech, IPlayerMechBattleUI battleUI, Camera camera, Vector3 startPosition, Quaternion startRotation) : base(name, mech, startPosition, startRotation)
        {
            _BattleUI = battleUI;
            _Camera = camera;

            _CameraPivot = (mech as Mech).GetComponentInChildren<CameraPivot>();
            
            _CameraPivotStartPosition = _CameraPivot.transform.localPosition;

            _BattleUI.camera = camera;

            _BattleUI.targetHandlerUI.Hide();
        }

        public override void Start()
        {
            mech.health.ModifyValue(mech.health.MaxValue);
            mech.boost?.boostValue.ModifyValue(mech.boost.boostValue.MaxValue);

            base.Start();

            _BattleUI.SetMech(mech);
            _BattleUI.Show();
            _BattleUI.targetHandlerUI.Show();
        }

        public override void Stop()
        {
            base.Stop();
            
            _BattleUI.targetHandlerUI.Hide();
        }

        public override void Reset()
        {
            base.Reset();
            
            _CameraPivot.transform.localPosition = _CameraPivotStartPosition;
            _CameraPivot.transform.localRotation = Quaternion.identity;
        }
    }
}