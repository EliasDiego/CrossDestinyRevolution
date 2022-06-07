using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HurtBox : MonoBehaviour, IHurtBox
	{
		[SerializeField] BoxCollider m_boxCollider;
		[SerializeField] Vector3 m_hitBoxSize = Vector3.one;
		[SerializeField] bool m_active = true;
		[SerializeField] ActiveCharacter m_owner = null;

		public bool Active { get => m_active; }
		public ActiveCharacter Owner { get => m_owner; } //Make ActiveCharacter
		public Transform Transform { get => transform; }

		void Update()
		{
			m_boxCollider.size = m_hitBoxSize;
		}

		public void HurtBoxResponse(float damage)
		{
			m_owner.health.TakeDamage(damage);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			Gizmos.DrawCube(Vector3.zero, new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z));
		}
	}
}

