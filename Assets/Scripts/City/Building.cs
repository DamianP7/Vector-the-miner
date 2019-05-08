using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Settings", order = -1)]
    [SerializeField] protected VoidDelegate buildingAction;
    [SerializeField] protected string buildingText;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Player")
            CenterButton.Instance.AddAction(buildingAction, buildingText);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            CenterButton.Instance.RemoveAllActions();
    }
}
