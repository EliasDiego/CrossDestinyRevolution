using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;
using CDR.CameraSystem.NewStuff;
using Cinemachine;

namespace CDR.VersusSystem
{
    public class PlayerParticipant : Participant, ICameraParticipant
    {
        private IPlayerMechBattleUI _BattleUI;
        private CameraHandler _CameraHandler;
        
        public Camera camera => _CameraHandler.camera;
        public IPlayerMechBattleUI battleUI => _BattleUI;

        public CinemachineVirtualCamera virtualCamera => _CameraHandler.virtualCamera;

        public PlayerParticipant(string name, IMech mech, IPlayerMechBattleUI battleUI, CameraHandler cameraHandler, Vector3 startPosition, Quaternion startRotation) : base(name, mech, startPosition, startRotation)
        {
            _BattleUI = battleUI;
            _CameraHandler = cameraHandler;
            _CameraHandler.Enable();

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
    }
}