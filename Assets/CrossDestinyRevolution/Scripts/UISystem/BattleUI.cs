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

        [SerializeField] TargetHandlerUI _targetHandlerUI;

        ICooldownActionUI _specialAttack1AttackUI;
        ICooldownActionUI _specialAttack2AttackUI;
        ICooldownActionUI _specialAttack3AttackUI;
        bool _isShown;

        public IValueRangeUI healthUI => healthBar;
        public IValueRangeUI boostUI => boostBar;
        public ITargetHandlerUI targetHandlerUI => _targetHandlerUI;
        public ICooldownActionUI specialAttack1AttackUI => _specialAttack1AttackUI;
        public ICooldownActionUI specialAttack2AttackUI => _specialAttack2AttackUI;
        public ICooldownActionUI specialAttack3AttackUI => _specialAttack3AttackUI;
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
            //specialAttack1AttackUI.SetCooldownAction(mech.specialAttack1);
            //specialAttack2AttackUI.SetCooldownAction(mech.specialAttack2);
            //specialAttack3AttackUI.SetCooldownAction(mech.specialAttack3);
            //mech.targetHandler.onSwitchTarget += _targetHandlerUI.SetTargetData;
        }

        private void Update()
        {
            
        }
    }
}

