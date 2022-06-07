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

		public void Explosion()
		{

		}
	}

	public class HurtData
	{
		public void TakeDamage()
		{

		}
	}

	public interface IHitResponder
	{
		public float Damage { get; }
		public bool CheckHit();
		public void Response();
	}
	public interface IHitDetector
	{
		public IHitResponder HitResponder { get; set; }
		public void CheckHit();
	}
	public interface IHurtResponder
	{
		public bool CheckHit();
		public void Response(float damage);
	}
	public interface IHurtBox
	{
		public bool Active { get; }
		public GameObject Owner { get; }
		public Transform Transform { get; }
		public IHurtResponder hurtResponder { get; set; }
		public bool CheckHit();
		public void Response(float damage);
	}
}
