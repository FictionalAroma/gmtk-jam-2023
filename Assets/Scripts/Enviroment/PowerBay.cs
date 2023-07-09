using System;
using UnityEngine;

namespace Assets.Scripts.Enviroment
{
    public class PowerBay : MonoBehaviour
	{
		[SerializeField] private PowerCell attachedCell = null;
		public bool HasCell => attachedCell != null;
		public int CellPower => attachedCell != null ? attachedCell.CurrentPower : 0;

		public Action<float> CellAdded;
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
			CellAdded?.Invoke(newCell.CurrentPower);
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

		public int UsePower(int power)
		{
			if (attachedCell != null && attachedCell.CurrentPower > 0)
			{
				attachedCell.UsePower(power);
				return CellPower;
			}

			return CellPower;
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