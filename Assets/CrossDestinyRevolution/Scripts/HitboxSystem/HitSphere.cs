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
		private IHitResponder m_hitResponder;

		public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

		public void CheckHit()
		{
			Vector3 _direction = transform.up;
			Vector3 _center = transform.TransformPoint(m_SphereCollider.center);
			Vector3 _start = transform.position;

			RaycastHit[] _hits = Physics.SphereCastAll(_start, m_radius, _direction, m_radius, m_layerMask);

			CheckCollisions(_hits, _center);
		}

		void CheckCollisions(RaycastHit[] _hits, Vector3 _center)
		{
			HitData _hitdata;
			IHurtBox _hurtbox;
			foreach (RaycastHit _hit in _hits)
			{
				_hurtbox = _hit.collider.GetComponent<IHurtBox>();
				if (_hurtbox != null)
				{
					if (_hurtbox.Active)
					{
						_hitdata = new HitData
						{
							damage = m_hitResponder == null ? 0 : m_hitResponder.Damage,
							hitPoint = _hit.point == Vector3.zero ? _center : _hit.point,
							hitNormal = _hit.normal,
							hurtbox = _hurtbox,
							hitDetector = this
						};

						if (_hitdata.Validate())
						{
							_hitdata.hitDetector.HitResponder?.Response(_hitdata);
							_hitdata.hurtbox.hurtResponder?.Response(_hitdata);
						}
					}
				}
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



