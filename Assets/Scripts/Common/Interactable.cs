using CommonComponents.Interfaces;
using UnityEngine;

namespace CommonComponents
{
	public abstract class Interactable : MonoBehaviour
	{
		[SerializeField] private string actionText;
		[SerializeField] private string ItemName;

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.TryGetComponent<InteractableActor>(out var c))
			{
				c.SetInteractableObject(this);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.TryGetComponent <InteractableActor>(out var c))
			{
				c.ResetInteractableObject(this);
			}
		}

		public abstract bool Action(InteractableActor actor);
	}
}