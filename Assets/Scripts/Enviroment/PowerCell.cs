using Common;
using UnityEngine;

public class PowerCell : Pickup
{
	[SerializeField] public int maxPower = 100;
	[SerializeField] public int CurrentPower = 100;

	private void Awake()
	{
		CurrentPower = Random.Range(maxPower / 2, maxPower);
	}

	public void UsePower(int power)
	{
		CurrentPower -= power;
		ClampResource();
	}

	private void ClampResource()
	{
		CurrentPower = Mathf.Clamp(CurrentPower, 0, maxPower);
	}

	public void Charge(int power)
	{
		CurrentPower = Mathf.Min(CurrentPower + power, maxPower);
		ClampResource();
	}

}
