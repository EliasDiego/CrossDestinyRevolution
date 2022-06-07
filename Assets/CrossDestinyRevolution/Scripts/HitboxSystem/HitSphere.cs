using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HitSphere : MonoBehaviour, IHitDetector
	{
		[SerializeField] SphereCollider m_SphereCollider;
		[SerializeField] LayerMask m_layerMask;

		[SerializeField] float m_radius = 0.025f;

		public IHitResponder m_hitResponder;
		public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

		public void CheckHit()
		{
			Vector3 _direction = transform.up;
			Vector3 _start = transform.position;

			RaycastHit[] _hits = Physics.SphereCastAll(_start, m_radius, _direction, m_radius, m_layerMask);

			foreach (RaycastHit _hit in _hits)
			{
				IHurtBox _hurtbox = _hit.collider.GetComponentInChildren<IHurtBox>();
				_hurtbox.hurtResponder.Response(m_hitResponder.Damage);
				HitResponder.Response();
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			Gizmos.DrawSphere(Vector3.zero, m_radius);
		}
	}
}



