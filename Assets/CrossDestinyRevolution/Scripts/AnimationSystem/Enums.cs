using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public enum ActionType
    {
        None = 0,
        RangeAttack = 1,
        MeleeAttack = 2,
        Shield = 3,
        SpecialAttack1 = 4,
        SpecialAttack2 = 5,
        SpecialAttack3 = 6
    }

    public enum StateType
    {
        None = 0,
        Stun = 1,
        Knockback = 2,
        Death = 3
    }

    public enum MoveType
    {
        None = 0,
        Movement = 1,
        HorizontalBoost = 2,
        VerticalBoost = 3
    }
}