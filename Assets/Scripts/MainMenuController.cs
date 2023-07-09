using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public void StartGameClick()
	{
		SceneManager.LoadScene(1);
	}
}