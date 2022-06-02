using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;
using CDR.AttackSystem;
using CDR.ActionSystem;
using CDR.MovementSystem;

namespace CDR.UISystem
{
    public interface IUIElement
    {
        bool isShown { get; }

        void Show();
        void Hide();
    }

    public interface ITargetHandlerUI : IUIElement
    {
        void SetTarget(IActiveCharacter target);
    }

    public interface ICooldownActionUI : IUIElement
    {
        void SetCooldownAction(ICooldownAction cooldownAction);
    }

    public interface IValueRangeUI : IUIElement
    {
        void SetValueRange(IValueRange valueRange);
    }

    public interface IPlayerMechBattleUI : IUIElement
    {
        IValueRangeUI healthUI { get; }
        IValueRangeUI boostUI { get; }
        ITargetHandlerUI targetHandlerUI { get; }
        ICooldownActionUI specialAttack1AttackUI { get; }
        ICooldownActionUI specialAttack2AttackUI { get; }
        ICooldownActionUI specialAttack3AttackUI { get; }
        
        void SetMech(IMech mech);
    }

    public interface IMenu : IUIElement
    {
        IMenu previousMenu { get; }
        void SwitchTo(IMenu nextMenu);
        void Back();
    }
}