using System.Collections.Generic;
using Assets.Scripts.Player;
using CommonComponents;
using CommonComponents.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Enviroment
{
	public class PowerPoint : Interactable
	{
		[SerializeField] private List<PowerBay> bays;
		[SerializeField] private Transform ejectPos;

		public override bool Action(InteractableActor actor)
		{
			var pc = actor as PlayerPickupController;
			if (pc != null)
			{
				var cell = pc.CurrentPickup as PowerCell;
				if (cell != null)
				{
					return AddCell(cell);
				}
			}

			return false;
		}

		private bool AddCell(PowerCell cell)
		{
			var bay = FindBay(false);
			if (bay != null)
			{
				bay = GetLowestPowerCell();

				EjectCell(bay);
			}

			PluginCell(bay, cell);
			return true;
		}

		private void PluginCell(PowerBay bay, PowerCell cell)
		{
			bay.PlugIn(cell);
			cell.EnablePhysics(false);
			var transform1 = bay.transform;
			cell.transform.SetPositionAndRotation(transform1.position, transform1.rotation);
		}

		private void EjectCell(PowerBay bay)
		{
			var eject = bay.Eject();
			eject.EnablePhysics(true);
			eject.transform.SetPositionAndRotation(ejectPos.position, Quaternion.identity);
		}

		private PowerBay GetLowestPowerCell()
		{
			PowerBay lowestBay = null;
			foreach (var bay in bays)
			{
				if (lowestBay == null || (bay.HasCell && bay.CellPower < lowestBay.CellPower))
				{
					lowestBay = bay;
				}
			}

			return lowestBay;
		}

		[CanBeNull]
		private PowerBay FindBay(bool occupied)
		{
			foreach (var bay in bays)
			{
				if (bay.HasCell == occupied) return bay;
			}
			return null;
		}

	}
}