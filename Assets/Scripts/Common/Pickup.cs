using Assets.Scripts.Player;
using CommonComponents;
using CommonComponents.Interfaces;
using UnityEngine;

namespace Common
{
	[RequireComponent(typeof(Rigidbody))]
	public class Pickup : Interactable
	{
		private Joint _joint = null;

		public void Connect(Rigidbody actor)
		{
			_joint = gameObject.AddComponent<FixedJoint>();
			_joint.connectedBody = actor;
			_joint.connectedMassScale = 0f;
			EnableColliders(false);
		}

		public override bool Action(InteractableActor actor)
		{
			var pc = actor as PlayerPickupController;
			if (pc != null)
			{
				pc.Pickup(this);
				return true;
			}

			return false;
		}

		public void Disconnect()
		{
			Destroy(_joint);
			_joint = null;
			EnableColliders(true);
		}

		public virtual void StopUse() {  }
		public virtual void Use() { }
		public void EnablePhysics(bool enable)
		{
			GetComponent<Rigidbody>().isKinematic = !enable;
			EnableColliders(enable);
		}

		private void EnableColliders(bool enable)
		{
			var colls = GetComponents<Collider>();
			foreach (var coll in colls)
			{
				coll.enabled = enable;
			}
		}
	}
}