using Assets.Scripts.Enviroment;
using Common;
using CommonComponents.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerPickupController : InteractableActor
	{
		private Pickup _currentPickup;
		public Pickup CurrentPickup => _currentPickup;

		[SerializeField] private InputReader inputReader;
		public Rigidbody RB { get; private set; }


		private void Awake()
		{
			inputReader.PlayerActionEvent += TryPickupOrDrop;
			RB = GetComponent<Rigidbody>();
		}

		private void TryPickupOrDrop(bool obj)
		{
			if (ActionCurrent())
			{

			}
		}

		public void Drop() => Drop(CurrentPickup);

		public void Drop(Pickup item)
		{
			_currentPickup = null;
			item.Disconnect();
		}

		public void Pickup(Pickup pickup)
		{
			if (CurrentPickup == null)
			{

				_currentPickup = pickup;
                
                pickup.Connect(RB);
			}
		}
	}
}