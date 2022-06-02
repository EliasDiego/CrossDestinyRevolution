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

		public bool Validate()
		{
			if (hurtbox != null)
			{
				if (hurtbox.CheckHit(this))
				{
					if (hurtbox.hurtResponder == null || hurtbox.hurtResponder.CheckHit(this))
					{
						if (hitDetector.HitResponder == null || hitDetector.HitResponder.CheckHit(this))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public interface IHitResponder
	{
		public float Damage { get; }
		public bool CheckHit(HitData data);
		public void Response(HitData data);
	}
	public interface IHitDetector
	{
		public IHitResponder HitResponder { get; set; }
		public void CheckHit();
	}
	public interface IHurtResponder
	{
		public bool CheckHit(HitData data);
		public void Response(HitData data);
	}
	public interface IHurtBox
	{
		public bool Active { get; }
		public GameObject Owner { get; }
		public Transform Transform { get; }
		public IHurtResponder hurtResponder { get; set; }
		public bool CheckHit(HitData hitData);
	}
}
