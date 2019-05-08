namespace GameWindows
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[RequireComponent(typeof(CanvasGroup))]
	public class Window : MonoBehaviour
	{
		[SerializeField]
		protected float changeSpeed = 0.5f;

		public float ChangeSpeed
		{
			get { return changeSpeed; }
		}

		private CanvasGroup canvasGroup;

		public void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public virtual void ShowWindow()
		{
            canvasGroup.interactable = true;
            StartCoroutine(FadeTo(1f, changeSpeed));
		}

		public virtual void HideWindow()
        {
            canvasGroup.interactable = false;
            StartCoroutine(FadeTo(0f, changeSpeed));
		}

		private IEnumerator FadeTo(float aValue, float aTime)
		{
			float beginAlpha = canvasGroup.alpha;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				canvasGroup.alpha = Mathf.Lerp(beginAlpha, aValue, t);
				yield return null;
			}
			ChangeAlpha(aValue);
		}


		public void ChangeAlpha(float newValue)
		{
			canvasGroup.alpha = newValue;

			if (newValue == 1)
			{
				canvasGroup.blocksRaycasts = true;
			}
			else if (newValue == 0)
			{
				canvasGroup.blocksRaycasts = false;
			}
			else
			{
				canvasGroup.blocksRaycasts = false;
			}
		}

		public bool IsHided()
		{
			return canvasGroup.alpha == 0;
		}

		public virtual void InstantShowWindow()
		{
			ChangeAlpha(1f);
		}

		public virtual void InstanstHideWindow()
		{
			ChangeAlpha(0f);
		}
	}

}