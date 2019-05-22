using System;
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

    /* TODO
     * Rekurencyjna funkcja w TilesOnMap do sprawdzania czy pod tilesem jest ziemia,
     * jeśli tak to nie uruchamia funkcji znowu tylko zwraca wytrzymałość
     * jeśli nie to uruchamia funkcję ze zmniejszonym licznikiem (ktory jest
     * graniczną wytrzymałością) dla tilesa obok i tak do skończenia licznika lub znalezienia
     * tilesa z ziemią pod nim. W tilesie który wywołał funkcję jako pierwszy sprawdzane jest
     * zwrócona wytrzymałość. Jeśli jest równa 0 to wali sie, jeśli równa 1 to pojawiają sie particle
     * z walącą sie ziemią (może dla 2 rzadkie particle).
     * 
     * Do tilesa trzeba dodać pole do którego będzie zapisywana wartość z rekurencji.
     * 
     */

    [Header("Variables")]
    [SerializeField] int maxBagCapacity;

    [Header("UI elements")]
    [SerializeField] BagUI bagUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Text cashText;
    [SerializeField] Text batteryText;
    [SerializeField] RectTransform batteryBar;

    [Header("Misc.")]
    [SerializeField] PlayerAnimations playerAnimations; // TODO: jeszcze nie wiem jak to zrobić. MYŚL!!
    [SerializeField] EnergyCosts energyCosts; // TODO: jeszcze nie wiem jak to zrobić. MYŚL!!

    public PlayerBag bag;
    public PlayerInventory inventory;
    public PlayerStats stats;
    public PlayerController controller;
    public PlayerMovement movement;
	public PlayerEquipment equipment;

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
        stats = new PlayerStats(energyCosts, cashText, batteryText, batteryBar);
        controller = new PlayerController();
        bag = new PlayerBag(bagUI, maxBagCapacity);
        inventory = new PlayerInventory(inventoryUI);
        movement = new PlayerMovement(playerAnimations);
		equipment = new PlayerEquipment();
    }

    internal void Coroutine(IEnumerator enumerable)
    {
        StartCoroutine(enumerable);
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