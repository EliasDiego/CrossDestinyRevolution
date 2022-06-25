using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.MovementSystem;

namespace CDR.AttackSystem
{
    public interface ICollisionShape
    {
        Vector3 position { get; }
        IActiveCharacter character { get; set; }
        
        event Action<IHitEnterData> onHitEnter;
        event Action<IHitExitData> onHitExit;
    }

    public interface IHitShape : ICollisionShape
    {
        LayerMask hitLayer { get; set; }
        IController controller { get; set; }
    }

    public interface IHurtShape : ICollisionShape
    {
        void HitEnter(IHitEnterData hitData);
        void HitExit(IHitExitData hitData);
    }

    public interface ICollisionData
    {
        IHitShape hitShape  { get; }
        IHurtShape hurtShape { get; }
    }

    public interface IHitEnterData : ICollisionData
    {
        Vector3 collisionPoint { get; }
        Vector3 collisionNormal { get; }
    }

    public interface IHitExitData : ICollisionData
    {
        
    }
}