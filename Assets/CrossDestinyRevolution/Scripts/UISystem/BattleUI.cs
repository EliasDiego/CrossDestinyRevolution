using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.UISystem
{
    public class BattleUI : MonoBehaviour, IPlayerMechBattleUI
    {
        [SerializeField] ProgressBar healthBar;
        [SerializeField] ProgressBar boostBar;

        [SerializeField] CooldownActionUI specialAttack1;
        [SerializeField] CooldownActionUI specialAttack2;
        [SerializeField] CooldownActionUI specialAttack3;

        [SerializeField] TargetHandlerUI _targetHandlerUI;
        [SerializeField] TargetHandlerUI _targetHealthUI;

        bool _isShown;

        public IValueRangeUI healthUI => healthBar;
        public IValueRangeUI boostUI => boostBar;
        public ITargetHandlerUI targetHandlerUI => _targetHandlerUI;
        public ITargetHandlerUI targetHealthHandlerUI => _targetHealthUI;
        public ICooldownActionUI specialAttack1AttackUI => specialAttack1;
        public ICooldownActionUI specialAttack2AttackUI => specialAttack2;
        public ICooldownActionUI specialAttack3AttackUI => specialAttack3;
        public bool isShown => _isShown;

        public void Hide()
        {
            
        }

        public void Show()
        {

        }

        public void SetMech(IMech mech)
        {
            healthUI.SetValueRange(mech.health);
            boostUI.SetValueRange(mech.boost.boostValue);
            
            _targetHandlerUI.SetTarget(mech.targetHandler.GetCurrentTarget());
            _targetHealthUI.SetTarget(mech.targetHandler.GetCurrentTarget());

            if(mech.specialAttack1!= null)
                specialAttack1AttackUI.SetCooldownAction(mech.specialAttack1);
            if(mech.specialAttack2 != null)
                specialAttack2AttackUI.SetCooldownAction(mech.specialAttack2);
            if(mech.specialAttack3 != null)
                specialAttack3AttackUI.SetCooldownAction(mech.specialAttack3);
            
            mech.targetHandler.onSwitchTarget += _targetHealthUI.SetTarget;
            mech.targetHandler.onSwitchTarget += _targetHandlerUI.SetTarget;
        }

        private void Update()
        {
            
        }
    }
}

