using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Instance
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
                if (instance == null)
                    Debug.LogError("No Player found on the scene!");
            }
            return instance;
        }
    }
    #endregion


    [Header("Variables")]
    [SerializeField] int maxBagCapacity;

    [Header("UI elements")]
    [SerializeField] BagUI bagUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Text cashText;
    [SerializeField] RectTransform batteryBar;

    [Header("Misc.")]
    [SerializeField] PlayerAnimations playerAnimations; // TODO: jeszcze nie wiem jak to zrobić. MYŚL!!

    public PlayerBag bag;
    public PlayerInventory inventory;
    public PlayerStats stats;
    public PlayerController controller;
    public PlayerMovement movement;

    [SerializeField] float timeToNextMove;
    public bool canMove = true;

    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    private void Awake()
	{
		NewGame();
	}

	public void NewGame()
	{
		controller = new PlayerController();
		stats = new PlayerStats(cashText, batteryBar);
		bag = new PlayerBag(bagUI, maxBagCapacity);
		inventory = new PlayerInventory(inventoryUI);
        movement = new PlayerMovement(playerAnimations);
	}

    public void WaitForMove()
    {
        StartCoroutine(WaitForMoveCoroutine());
    }

    public void Moving(float time)
    {
        StartCoroutine(MovingCoroutine(time));
    }

    IEnumerator WaitForMoveCoroutine()
    {
        canMove = false;
        yield return new WaitForSeconds(timeToNextMove);
        canMove = true;
    }

    public IEnumerator MovingCoroutine(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    
}


public enum Direction
{
    Down,
    DownLeft,
    Left,
    UpLeft,
    Up,
    UpRight,
    Right,
    DownRight,
    Center
}

public enum Action
{
    Move, Dig, Climb, JumpDown, Nothing, DigAndRope // TooLow, TooHight
}