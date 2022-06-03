using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.StateSystem
{
    public interface IState
    {
        IMech sender { get; set; }
        IMech receiver { get; set; }

        void StartState();
        void EndState();
    }

    public interface IStun : IState
    {
        float time { get; }
    }

    public interface IKnockback : IStun
    {
        float distance { get; } 
    }
}