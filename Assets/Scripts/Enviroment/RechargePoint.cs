using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enviroment
{
	public class RechargePoint : PowerPoint
	{
		private bool _exit;

		[SerializeField] private int rechargeAmount = 5;
		[SerializeField] private float rechargeTick = 2.5f;

		public void Start()
		{
			StartCoroutine(Recharge());
		}

		private IEnumerator Recharge()
		{
			while (!_exit)
			{
				foreach (var bay in bays)
				{
					if (bay.HasCell && !bay.Recharge(rechargeAmount))
					{
						EjectCell(bay);
					}
				}

				yield return new WaitForSeconds(rechargeTick);
			}
		}

		private void OnDestroy()
		{
			_exit = true;
			StopAllCoroutines();
		}
	}
}