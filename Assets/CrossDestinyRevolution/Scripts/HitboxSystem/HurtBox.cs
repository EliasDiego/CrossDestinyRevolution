using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HurtBox : MonoBehaviour, IHurtBox
	{
		[SerializeField] BoxCollider m_BoxCollider;
		[SerializeField] SphereCollider m_SphereCollider;
		[SerializeField] bool isSphere;

		[SerializeField] Vector3 m_hitBoxSize = Vector3.one;
		[SerializeField] float m_radius = 0.025f;

		[SerializeField] bool m_active = true;
		[SerializeField] GameObject m_owner = null;
		IHurtResponder m_hurtResponder;// Make Array

		public bool Active {get => m_active; } 
		public GameObject Owner { get => m_owner; } //Make ActiveCharacter
		public Transform Transform { get => transform; }
		public IHurtResponder hurtResponder { get => m_hurtResponder; set => m_hurtResponder = value; } //Make Array

		private void Update() {
			
			SwapCollider();
		}

		public void SwapCollider()
		{
			if (!isSphere)
			{
				m_BoxCollider.enabled = true;
				m_SphereCollider.enabled = false;

				m_BoxCollider.size = m_hitBoxSize;
			}

			if (isSphere)
			{
				m_BoxCollider.enabled = false;
				m_SphereCollider.enabled = true;

				m_SphereCollider.radius = m_radius;
			}
		}

		public bool CheckHit(HitData hitData)
		{
			if(m_hurtResponder == null)
			{
				//Debug.LogWarning("No responder");
			}

			this.gameObject.GetComponent<ActiveCharacter>().health.TakeDamage(hitData.damage);

			return true;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

			if (!isSphere)
				Gizmos.DrawCube(Vector3.zero, new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z));
			if (isSphere)
				Gizmos.DrawSphere(Vector3.zero, m_radius);
		}
	}
}

