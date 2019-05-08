using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPanel : MonoBehaviour
{
	[SerializeField] RectTransform panel;	// panel, ktorym chcesz poruszac
	[SerializeField] Direction direction;	// kierunek wysuwania panelu
	[SerializeField] float panelSpeed;		// predkosc wysuwania
	[SerializeField] float offset;			// przesuniecie wysunietego panelu (gdy chcemy zeby, np jego poczatek byl schowany)
	[SerializeField] bool startShown;		// poczatkowy stan panelu

	float shownPosition, hiddenPosition;
	bool opened;

	private void Awake()
	{
		opened = startShown;
		if (direction == Direction.Down)
		{
			hiddenPosition = panel.anchoredPosition.y;
			shownPosition = hiddenPosition - panel.rect.height;
		}
		else if (direction == Direction.Up)
		{
			hiddenPosition = panel.anchoredPosition.y;
			shownPosition = hiddenPosition + panel.rect.height;
		}
		else if (direction == Direction.Left)
		{
			hiddenPosition = panel.anchoredPosition.x;
			shownPosition = hiddenPosition - panel.rect.width;
		}
		else if (direction == Direction.Right)
		{
			hiddenPosition = panel.anchoredPosition.x;
			shownPosition = hiddenPosition + panel.rect.width;
		}
	}

	private void Update()
	{
		//Debug.Log("panel " + '(' + opened + "): " + panel.anchoredPosition.x);	// position debugging

		if (direction == Direction.Down)
		{
			if (opened)
			{
				if (panel.anchoredPosition.y > shownPosition + offset)
					panel.position = new Vector3(panel.position.x, panel.position.y - panelSpeed);
			}
			else if (!opened)
			{
				if (panel.anchoredPosition.y < hiddenPosition)
					panel.position = new Vector3(panel.position.x, panel.position.y + panelSpeed);
			}
		}
		else if (direction == Direction.Up)
		{
			if (opened)
			{
				if (panel.anchoredPosition.y < shownPosition + offset)
					panel.position = new Vector3(panel.position.x, panel.position.y + panelSpeed);
			}
			else if (!opened)
			{
				if (panel.anchoredPosition.y > hiddenPosition)
					panel.position = new Vector3(panel.position.x, panel.position.y - panelSpeed);
			}
		}
		else if (direction == Direction.Left)
		{
			if (opened)
			{
				if (panel.anchoredPosition.x > shownPosition + offset)
					panel.position = new Vector3(panel.position.x - panelSpeed, panel.position.y);
			}
			else if (!opened)
			{
				if (panel.anchoredPosition.x < hiddenPosition)
					panel.position = new Vector3(panel.position.x + panelSpeed, panel.position.y);
			}
		}
		else if (direction == Direction.Right)
		{
			if (opened)
			{
				if (panel.anchoredPosition.x < shownPosition + offset)
					panel.position = new Vector3(panel.position.x + panelSpeed, panel.position.y);
			}
			else if (!opened)
			{
				if (panel.anchoredPosition.x > hiddenPosition)
					panel.position = new Vector3(panel.position.x - panelSpeed, panel.position.y);
			}
		}
	}

	/*
#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		if (direction == Direction.Left)
		{
			Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].smallGroup.minimumDepth),
			new Vector3(pos, -squareSize * ores.ores[i].smallGroup.minimumDepth));
		}
		else if (direction == Direction.Right)
		{

		}

	}
#endif*/

	public void MovePanel()
	{
		opened = !opened;
	}

	public void OpenPanel()
	{
		opened = true;
	}

	public void ClosePanel()
	{
		opened = false;
	}

	enum Direction
	{
		Up, Down, Left, Right
	}
}
