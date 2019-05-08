namespace ScrollingList
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

    /// <summary>
    /// Lista, ktora moze byc przewijana.
    /// </summary>
    /// <typeparam name="ItemType">Klasa, w ktorej liscie sa przechowywane dane przedmiotow</typeparam>
    /// <typeparam name="SlotType">Klasa slotu, ktory jest wyswietlany na liscie</typeparam>
	public abstract class ScrollingListButtons<ItemType, SlotType> : MonoBehaviour
	{
		public List<ItemType> listOfItems;

		public SlotType[] itemSlots;

		[SerializeField] protected Button upButton, downButton;

		protected int firstIndex, lastIndex;

		int FirstIndex
		{
			get
			{
				return firstIndex;
			}
			set
			{
				firstIndex = value;
				if (firstIndex <= 0)
				{
					upButton.gameObject.SetActive(false);
				}
				else
				{
					upButton.gameObject.SetActive(true);
				}
			}
		}

		int LastIndex
		{
			get
			{
				return lastIndex;
			}
			set
			{
				lastIndex = value;
				if (lastIndex >= listOfItems.Count)
				{
					downButton.gameObject.SetActive(false);
				}
				else
				{
					downButton.gameObject.SetActive(true);
				}
			}
		}

		void Awake()
		{
			upButton.onClick.AddListener(ScrollUp);
			downButton.onClick.AddListener(ScrollDown);
		}

		public void ShowItems()
		{
			FirstIndex = 0;
			LastIndex = itemSlots.Length;

			UpdateShowItems();

			if (listOfItems.Count > itemSlots.Length)
			{
				downButton.gameObject.SetActive(true);
			}
		}

		public void UpdateShowItems()
		{
			int slot = 0;
			int item = FirstIndex;
			while (slot < itemSlots.Length)
			{
				if (slot < listOfItems.Count)
				{
					ShowItem(itemSlots[slot], listOfItems[item]);
					item++;
				}
				else
					DisableSlot(itemSlots[slot]);

				slot++;
			}
		}

		/// <summary>
		/// Activate slot and update it.
		/// </summary>
		/// <param name="slot">Slot to change.</param>
		/// <param name="item">Item to show.</param>
		protected abstract void ShowItem(SlotType slot, ItemType item);

		protected abstract void DisableSlot(SlotType slot);

		public void ScrollDown()
		{
			FirstIndex++;
			LastIndex++;
			UpdateShowItems();
		}

		public void ScrollUp()
		{
			FirstIndex--;
			LastIndex--;
			UpdateShowItems();
		}
	}
}