using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.AttackSystem
{
    public interface ICollisionShape : IShape
    {
        IActiveCharacter character { get; set; }
        Collider collider { get; }
        
        event Action<IHitData> onHitEnter;
        event Action<IHitData> onHitExit;
    }

    public interface IHitShape : ICollisionShape
    {
        IHurtShape[] intersectedHurtShapes { get; }
    }

    public interface IHurtShape : ICollisionShape
    {
        void HitEnter(IHitData hitData);
        void HitExit(IHitData hitData);
    }

    public interface IHitData
    {
        IHitShape hitShape  { get; }
        IHurtShape hurtShape { get; }
    }

    public interface IShape
    {
        Vector3 center { get; set;}
    }

    public interface IBox : IShape
    {
        Vector3 size { get; set; }
    }

    public interface ISphere : IShape
    {
        float radius { get; set; }
    }
}