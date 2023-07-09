using UnityEngine;

namespace Assets.Scripts.Enviroment
{
    public class PowerBay : MonoBehaviour
	{
		private PowerCell _attachedCell = null;

		public bool HasCell => _attachedCell != null;
		public int CellPower => _attachedCell != null ? _attachedCell.CurrentPower : 0;

		public void PlugIn(PowerCell newCell)
		{
			_attachedCell = newCell;
		}

		public PowerCell Eject()
		{

			if (_attachedCell)
			{
				var cell = _attachedCell;
				_attachedCell = null;
				return cell;
			}

			return null;
		}

		public bool UsePower(int power)
		{
			if (_attachedCell != null && _attachedCell.CurrentPower > 0)
			{
				_attachedCell.UsePower(power);
				return true;
			}
			return false;
		}

		public bool Recharge(int amount)
		{
			if (_attachedCell != null && _attachedCell.CurrentPower < 100)
			{
				_attachedCell.Charge(amount);
				return true;
			}

			return false;
		}
	}
}