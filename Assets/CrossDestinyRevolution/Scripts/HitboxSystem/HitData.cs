using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HitData
	{
		public float damage;
		public Vector3 hitPoint;
		public Vector3 hitNormal;
		public IHurtBox hurtbox;
		public IHitDetector hitDetector;
	}

	public class HurtData
	{
		//put variables that is needed by hurtbox
	}

	public interface IHitResponder
	{
		public float Damage { get; }
		public void HitBoxResponse();
	}
	public interface IHitDetector
	{
		public IHitResponder HitResponder { get; set; }
		public void HitBoxCheckHit();
	}
	public interface IHurtBox
	{
		public bool Active { get; }
		public ActiveCharacter Owner { get; }
		public void HurtBoxResponse(float damage);
	}
}
