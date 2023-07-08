using System;
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
		private void Awake()
		{
		}

		public void Connect(Rigidbody actor)
		{
			_joint = gameObject.AddComponent<FixedJoint>();
			_joint.connectedBody = actor;
			_joint.connectedMassScale = 0f;
		}

		public override void Action(InteractableActor actor)
		{
			var pc = actor as PlayerPickupController;
			if (pc != null)
			{
				pc.Pickup(this);
			}
		}

		public void Disconnect()
		{
			Destroy(_joint);
			_joint = null;
		}
	}
}