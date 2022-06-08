using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public struct Participant : IParticipant
    {
        IMech _Mech;
        IPlayerMechBattleUI _BattleUI;
        Camera _Camera;
        Vector3 _StartPosition;

        public int score { get; set; }
        public IMech mech => _Mech;
        public IPlayerMechBattleUI battleUI => _BattleUI;
        public Camera camera => _Camera;

        public Participant(IMech mech, IPlayerMechBattleUI battleUI, Camera camera, Vector3 startPosition)
        {
            _Mech = mech;
            _BattleUI = battleUI;
            _Camera = camera;
            _StartPosition = startPosition;

            score = 0;
        }

        public void Reset()
        {
            mech.health.ModifyValue(_Mech.health.MaxValue);
            mech.boost?.boostValue.ModifyValue(_Mech.boost.boostValue.MaxValue);

            mech.input?.DisableInput();
            mech.movement?.End();
            mech.boost?.End();
            mech.targetHandler?.End();
            mech.meleeAttack?.End();
            mech.rangeAttack?.End();
            mech.specialAttack1?.End();
            mech.specialAttack2?.End();
            mech.specialAttack3?.End();

            mech.controller?.SetVelocity(Vector3.zero);
            
            (mech as Mech).transform.position = _StartPosition;
        }

        public void Start()
        {
            mech.input?.EnableInput();
            mech.targetHandler?.Use();
            mech.movement?.Use();

            // battleUI.SetMech(mech);
        }
    }
}