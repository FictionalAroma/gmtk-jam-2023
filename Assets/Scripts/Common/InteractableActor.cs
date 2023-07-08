using UnityEngine;

namespace CommonComponents.Interfaces
{
	public class InteractableActor : MonoBehaviour
	{
		private Interactable _interactiveObject;

		public void SetInteractableObject(Interactable interactiveObject)
		{
			_interactiveObject = interactiveObject;
			Debug.Log($"CurrentObject = {_interactiveObject}");

		}

		public void ResetInteractableObject(Interactable interactable)
		{
			if (_interactiveObject == interactable)
			{
				_interactiveObject = null;
			}
		}

		public void ActionCurrent()
		{
			if (_interactiveObject != null)
			{
				_interactiveObject.Action(this);
			}
		}
	}
}