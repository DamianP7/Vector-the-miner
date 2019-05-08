using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToPlace : MonoBehaviour
{
	Element item;
	SpriteRenderer spriteRenderer;

	bool isActive = false;
	private Vector3 startSwipe;   //First touch position
	private Vector3 stopSwipe;   //Last touch position
	public float dragDistance;  //minimum distance for a swipe to be registered

	void Start()
	{
		dragDistance = Screen.height * 5 / 100; //dragDistance is 5% height of the screen
		spriteRenderer = GetComponent<SpriteRenderer>();    //TODO: temp?
	}

	private void Update()
	{
		if (isActive)
		{
			TouchSwipe();
			MouseSwipe();
		}
	}

	private void TouchSwipe()
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				startSwipe = touch.position;
				stopSwipe = touch.position;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				stopSwipe = touch.position;
				transform.position = MapManager.Instance.CheckAndGetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				stopSwipe = touch.position;

				if (Mathf.Abs(stopSwipe.x - startSwipe.x) > dragDistance
					|| Mathf.Abs(stopSwipe.y - startSwipe.y) > dragDistance)
				{
					//swiped here
				}
				else
				{
					PlaceItem();
				}
			}
		}
	}

	private void MouseSwipe()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startSwipe = Input.mousePosition;
			stopSwipe = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			stopSwipe = Input.mousePosition;

			if (Mathf.Abs(stopSwipe.x - startSwipe.x) > dragDistance
				|| Mathf.Abs(stopSwipe.y - startSwipe.y) > dragDistance)
			{
				//swiped here
			}
			else
			{
				PlaceItem();
			}
		}
		else if (Input.GetMouseButton(0))
		{
			stopSwipe = Input.mousePosition;
			transform.position = MapManager.Instance.CheckAndGetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}

	public void SpawnItem(Element item)
	{
		InputManager.Instance.StopMovement();
		isActive = true;
		this.item = item;
		spriteRenderer.sprite = item.sprite;

		Vector3 pos = Player.Instance.transform.position;
		transform.position = new Vector3(pos.x, pos.y + MapManager.Instance.tileSize);
	}

	public void PlaceItem()
	{
		if (MapManager.Instance.PlaceItem(transform.position, item))
		{
			Debug.Log("Item Placed");
			HideObject();
			InputManager.Instance.ChangeToPlayerMovement();
		}
	}

	void HideObject()
	{
		isActive = false;
		transform.position = new Vector3(0, 100 * MapManager.Instance.tileSize);
	}

	/*

	bool moving = true;

	void Update()
	{


		// To będzie miało sens jak będę przenosił obiekt między Canvasem a MainScreenem
		if (moving)
		{

			// Bit shift the index of the layer (8) to get a bit mask
			//int layerMask = 1 << 8;

			// This would cast rays only against colliders in layer 8.
			// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
			//	layerMask = ~layerMask;

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))//, layerMask))
			{
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				Debug.Log("Did Hit");
			}
			else
			{
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
				Debug.Log("Did not Hit");
			}
		}
	}*/
}
