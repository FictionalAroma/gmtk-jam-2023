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
		[SerializeField] protected List<PowerBay> bays;
		[SerializeField] private Transform ejectPos;
		[SerializeField] private PowerPointUIController uiController;
		
		[SerializeField] private float powerDrainRate = 1f;

		private void Start()
		{
			//call use power by drain rate
			//if power is 0, eject cell
			//if power is 0 and no cell, disable powerpoint
			
			InvokeRepeating(nameof(UsePower), 0, powerDrainRate);
		}

		public void UsePower()
		{
			foreach (var bay in bays)
			{
				bay.UsePower((int)powerDrainRate);
				uiController.SetStatusBarValue(bay.CellPower);
				if(bay.HasCell)
					EjectCell(bay);
			}
		}

		public override bool Action(InteractableActor actor)
		{
			var pc = actor as PlayerPickupController;
			if (pc != null)
			{
				var cell = pc.CurrentPickup as PowerCell;
				if (cell != null)
				{
					pc.Drop();
					return AddCell(cell);
				}
			}

			return false;
		}
		
		public void SetPowerPointEnergyValue(float value)
		{
			uiController.SetStatusBarValue(value);
		}

		private bool AddCell(PowerCell cell)
		{
			var bay = FindBay(false);
			if (bay == null)
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
		}

		protected void EjectCell(PowerBay bay)
		{
			var eject = bay.Eject();
			eject.EnablePhysics(true);
			eject.transform.SetPositionAndRotation(ejectPos.position, Quaternion.identity);
		}

		private PowerBay GetLowestPowerCell()
		{
			PowerBay lowestBay = null;
			int lowestPower = 100;
			foreach (var bay in bays)
			{
				if (bay.HasCell && bay.CellPower < lowestPower)
				{
					lowestBay = bay;
					lowestPower = bay.CellPower;
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