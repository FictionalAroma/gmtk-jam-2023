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
			inputReader.PrimaryFireEvent += HandlePrimaryFire;
			RB = GetComponent<Rigidbody>();
		}

		private void TryPickupOrDrop(bool obj)
		{
			if (!ActionCurrent())
			{
				Drop();
			}
		}

		private void HandlePrimaryFire(bool shoot)
		{
			if (_currentPickup != null)
			{
				if (shoot)
				{

					_currentPickup.Use();
				}
				else
				{
					_currentPickup.StopUse();
				}
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

		public void AimHand(Vector3 aimDir, float angle)
		{
			this.transform.localPosition = aimDir;
			if (_currentPickup != null)
			{
				_currentPickup.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);			}
		}
	}
}