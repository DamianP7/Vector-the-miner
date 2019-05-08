namespace GameWindows
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class WindowsController : MonoBehaviour
	{
		#region Instance
		private static WindowsController instance;
		public static WindowsController Instance
		{
			get
			{
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType<WindowsController>();

					if (instance == null)
					{
						print("No WindowsController found on the scene!");
					}
				}

				return instance;
			}
		}
		#endregion

		[Header("Shop windows")]
		public Window oreSoreWindow;
		public Window generalStoreWindow;

		private Window openedWindow;

		public void Start()
		{
			//TouchScreenKeyboard.hideInput = false;
			openedWindow = null;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (openedWindow != null)
				{
					CloseWindow();
				}
			}
		}

		public void CloseWindow()
		{
			if (openedWindow != null)
			{
				openedWindow.HideWindow();
				openedWindow = null;
				InputManager.Instance.StartMovement(); 
			}
		}

		public void ShowWindow(Window window)
		{
			InputManager.Instance.StopMovement();
			openedWindow = window;
			window.ShowWindow();
		}

		public void ChangeWindow(Window window)
		{
			InputManager.Instance.StopMovement();
			StartCoroutine(ChangeToWindow(window));
		}

		public void InstantChangeWindow(Window window)
		{
			InputManager.Instance.StopMovement();
			openedWindow.InstanstHideWindow();
			openedWindow = window;
			window.InstantShowWindow();
		}

		private IEnumerator ChangeToWindow(Window window)
		{
			openedWindow.HideWindow();
			window.ShowWindow();
			openedWindow = window;
			yield return null;
		}
		/*
		public void BackToMenu()
		{
			SaveManager.Instance.SaveGame();
			SceneManager.LoadScene("MainMenu");
		}*/

		/*
		public void FadeOutAndBack(float time)
		{
			fadeScreen.FadeInit(null, false, false);
			StartCoroutine(DoFadeOutAndBack(time));
		}

		private IEnumerator DoFadeOutAndBack(float time)
		{
			ChangeScreen(fadeScreen);
			yield return new WaitForSeconds(time);
			ChangeScreen(mainScreen);
		}

		public void FadeOutForNewScene(Sprite sprite)
		{
			fadeScreen.FadeInit(sprite, true, true);
			ChangeScreen(fadeScreen);
		}*/
		/*
		public bool IsMainScreenActive()
		{
			return actualScreen == mainScreen;
		}*/
	}

}