using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HurtBox : MonoBehaviour, IHurtBox, IHurtResponder
	{
		[SerializeField] BoxCollider m_boxCollider;
		[SerializeField] Vector3 m_hitBoxSize = Vector3.one;
		[SerializeField] bool m_active = true;
		[SerializeField] GameObject m_owner = null;


		IHurtResponder m_hurtResponder;// Make Array

		public bool Active { get => m_active; }
		public GameObject Owner { get => m_owner; } //Make ActiveCharacter
		public Transform Transform { get => transform; }
		public IHurtResponder hurtResponder { get => m_hurtResponder; set => m_hurtResponder = value; } //Make Array

		void Update()
		{
			m_boxCollider.size = m_hitBoxSize;
			m_hurtResponder = this;
		}

		public bool CheckHit()
		{
			return true;
		}
		public void Response(float damage)
		{
			m_owner.GetComponent<ActiveCharacter>().health.TakeDamage(damage);
		}


		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			Gizmos.DrawCube(Vector3.zero, new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z));
		}
	}
}

