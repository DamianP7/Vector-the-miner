using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
	[SerializeField] Text capacity;

	[SerializeField] OresSettings oresSettings;
	[SerializeField] List<CanvasOre> oresOnBar;

	Color defaultColor;
	List<OreSpritePair> icons = new List<OreSpritePair>();
	List<OreInBag> ores;
	int listOffset;

	private void Awake()
	{
		defaultColor = Color.white;
		listOffset = 0;
	}

	void SetupOreIcons()
	{
		icons = new List<OreSpritePair>();
		foreach (OresSettings.OreGroup ore in oresSettings.ores)
		{
			OreSpritePair oreSprite = new OreSpritePair();
			oreSprite.ore = ore.oreType;
			oreSprite.sprite = ore.icon;
			icons.Add(oreSprite);
		}
	}

	public void UpdateBag(int spaceLeft, int maxCapacity, List<OreInBag> oreList)
	{
		UpdateOres(oreList);
		UpdateCapacity(spaceLeft, maxCapacity);
	}

	public void UpdateBag(int spaceLeft, int maxCapacity)
	{
		UpdateOres();
		UpdateCapacity(spaceLeft, maxCapacity);
	}

	void UpdateCapacity(int spaceLeft, int maxCapacity)
	{
		if (spaceLeft <= maxCapacity * 0.2f) // less than 20%
			capacity.color = Color.red;
		else
			capacity.color = defaultColor;
		capacity.text = (maxCapacity - spaceLeft).ToString() + '/' + maxCapacity.ToString();
	}

	void UpdateOres(List<OreInBag> oreList)
	{
		if (icons.Count == 0)
			SetupOreIcons();

		this.ores = oreList;

		for (int i = 0; i < oresOnBar.Count; i++)
		{
			if (i + listOffset < ores.Count)
			{
				oresOnBar[i].gameObject.SetActive(true);
				oresOnBar[i].icon.sprite = icons.Find(x => x.ore == ores[i + listOffset].ore).sprite;
				oresOnBar[i].quantity.text = ores[i + listOffset].quantity.ToString();

			}
			else
			{
				oresOnBar[i].gameObject.SetActive(false);
			}

		}
	}

	void UpdateOres()
	{
		for (int i = 0; i < oresOnBar.Count; i++)
		{
			oresOnBar[i].gameObject.SetActive(false);
		}
	}

	void MoveLeft()
	{
		listOffset = listOffset > 0 ? listOffset - 1 : 0;
	}

	void MoveRight()
	{
		listOffset = listOffset < ores.Count - 1 ? listOffset + 1 : ores.Count - 1;
	}

	[System.Serializable]
	class OreSpritePair
	{
		public Ore ore;
		public Sprite sprite;
	}

	[System.Serializable]
	class CanvasOre
	{
		public GameObject gameObject;
		public Image icon;
		public Text quantity;
	}
}
