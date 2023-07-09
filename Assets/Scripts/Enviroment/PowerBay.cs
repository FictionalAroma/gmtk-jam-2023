using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Enviroment
{
    public class PowerBay : MonoBehaviour
	{
		[SerializeField] private PowerCell attachedCell = null;

		public bool HasCell => attachedCell != null;
		public int CellPower => attachedCell != null ? attachedCell.CurrentPower : 0;

		private void Start()
		{
			if (attachedCell != null)
			{
				PlugIn(attachedCell);
			}
		}

		public void PlugIn(PowerCell newCell)
		{
			attachedCell = newCell;

			newCell.EnablePhysics(false);
			var transform1 = transform;
			newCell.transform.SetPositionAndRotation(transform1.position, transform1.rotation);

		}

		public PowerCell Eject()
		{

			if (attachedCell)
			{
				var cell = attachedCell;
				attachedCell = null;
				return cell;
			}

			return null;
		}

		public bool UsePower(int power)
		{
			if (attachedCell != null && attachedCell.CurrentPower > 0)
			{
				attachedCell.UsePower(power);
				return true;
			}
			return false;
		}

		public bool Recharge(int amount)
		{
			if (attachedCell != null && attachedCell.CurrentPower < 100)
			{
				attachedCell.Charge(amount);
				return true;
			}

			return false;
		}
	}
}