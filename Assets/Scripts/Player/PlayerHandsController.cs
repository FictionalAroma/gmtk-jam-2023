using Common;
using CommonComponents.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerHandsController : InteractableActor
	{
		GrapplingController grappling;
		private Pickup _currentPickup;
		public Pickup CurrentPickup => _currentPickup;
		public PlayerController _playerController;
		[SerializeField] private InputReader inputReader;
		public Rigidbody RB { get; private set; }


		private void Awake()
		{
			_playerController = GetComponentInParent<PlayerController>();
			inputReader.PlayerActionEvent += TryPickupOrDrop;
			inputReader.PrimaryFireEvent += HandlePrimaryFire;
			RB = GetComponent<Rigidbody>();
			grappling = FindObjectOfType<GrapplingController>();
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


		public void Drop()
        {
			if (_currentPickup != null)
			{
				_currentPickup.Disconnect();
				_currentPickup = null;
			}
		}
		public void Pickup(Pickup pickup)
		{
			if (CurrentPickup == null)
			{

				_currentPickup = pickup;
                
                pickup.Connect(RB);
			}
		}

		public void AimHand(Vector3 aimPos)
		{
			Vector3 directionToTarget = aimPos - this.transform.position;
			directionToTarget.z = 0;
			float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

			this.transform.SetLocalPositionAndRotation(directionToTarget.normalized, Quaternion.AngleAxis(angle - 90, Vector3.forward));
			if (this.transform.rotation.eulerAngles.z>0&& this.transform.rotation.eulerAngles.z<180)
			{
				if (grappling.hookHands[1].GetComponent<GrappleHook>().hookIsFree)
				{
                    grappling.hookHands[1].transform.position = this.transform.position;
                    grappling.hookHands[1].transform.LookAt(_playerController.aimIndicator.transform.position, Vector3.up);
                }
				grappling.hookHands[0].transform.position = new Vector3(_playerController.transform.position.x + 1, _playerController.transform.position.y + 0.3f, 0);
				grappling.hookHands[0].transform.rotation = Quaternion.Euler(0, 90, 0);
                
            }
			if (this.transform.rotation.eulerAngles.z>180)
			{
				if (grappling.hookHands[0].GetComponent<GrappleHook>().hookIsFree)
				{
                    grappling.hookHands[0].transform.position = this.transform.position;
                    grappling.hookHands[0].transform.LookAt(_playerController.aimIndicator.transform.position, Vector3.up);
                }
                
				grappling.hookHands[1].transform.position = new Vector3(_playerController.transform.position.x - 1, _playerController.transform.position.y+0.3f, 0);
                grappling.hookHands[1].transform.rotation = Quaternion.Euler(0, -90, 0);
                
            }
			if (_currentPickup != null)
			{
				_currentPickup.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
			}
		}

	}
}