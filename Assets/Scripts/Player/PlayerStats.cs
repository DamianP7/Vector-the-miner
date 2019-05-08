using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats
{
    int cash;

    public int Cash
    {
        get
        {
            return cash;
        }
        set
        {
            cash = value;
            cashText.text = cash.ToString();
        }
    }

    int battery, maxBattery;

    public int Battery
    {
        get
        {
            return battery;
        }
        set
        {
            battery = value;
            batteryBar.sizeDelta = new Vector2(290 * ((float)battery / maxBattery), batteryBar.sizeDelta.y);
        }
    }

    [SerializeField] Text cashText;
    [SerializeField] RectTransform batteryBar;
    [SerializeField] EnergyCosts energyCosts;
    
    public PlayerStats(Text cashText, RectTransform batteryBar, int cash = 0, int maxBattery = 10, int battery = 10)
    {
        this.cashText = cashText;
        this.Cash = cash;

        this.batteryBar = batteryBar;
        this.maxBattery = maxBattery;
        this.Battery = battery;
    }

    public bool CheckAction(Action action)
    {
        int cost = energyCosts.actionCosts.Find(x => x.action == action).cost;
        if (Battery >= cost)
            return true;
        else
            return false;
    }

    public bool DoAction(Action action)
    {
        int cost = energyCosts.actionCosts.Find(x => x.action == action).cost;
        if (Battery >= cost)
        {
            Battery -= cost;
            return true;
        }
        else
            return false;
    }
}
