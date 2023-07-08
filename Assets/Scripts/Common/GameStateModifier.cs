using System;
using System.Collections.Generic;
using CommonComponents.Interfaces;
using Management;
using UnityEngine;

namespace CommonComponents
{
    public class GameStateModifier : MonoBehaviour, IGameStateSubscriber
	{
		[SerializeField] private List<GameState> activeStateList;
		private MonoBehaviour[] _behaviourList;
		private void Start()
		{
			GameStateManager.Instance.Subscribe(this);
			_behaviourList = GetComponents<MonoBehaviour>();
		}

		private void OnDestroy()
		{
			GameStateManager.Instance.Unsubscribe(this);
		}

		public void OnStateChange(GameState newState)
		{
			bool enable = activeStateList.Contains(newState);
			foreach (var mb in _behaviourList)
			{
				mb.enabled = enable;
			}
		}
	}
}