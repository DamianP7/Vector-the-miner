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
            if (value > maxBattery)
                battery = maxBattery;
            else if (value < 0)
                battery = 0;
            else
                battery = value;

            batteryBar.sizeDelta = new Vector2(290 * ((float)battery / maxBattery), batteryBar.sizeDelta.y);
            batteryText.text = battery.ToString() + "/" + maxBattery.ToString();
        }
    }

    Text cashText;
    Text batteryText;
    RectTransform batteryBar;
    EnergyCosts energyCosts;

    public PlayerStats(EnergyCosts energyCosts, Text cashText, Text batteryText, RectTransform batteryBar, int cash = 0, int maxBattery = 10, int battery = 10)
    {
        this.cashText = cashText;
        this.Cash = cash;

        this.energyCosts = energyCosts;
        this.batteryText = batteryText;
        this.batteryBar = batteryBar;
        this.maxBattery = maxBattery;
        this.Battery = battery;
    }

    public bool CheckAction(Action action)
    {
        int cost = energyCosts.actionCosts.Find(x => x.action == action).cost;
        if (Battery >= cost)
        {
            return true;
        }
        else
        {
            Player.Instance.Coroutine(LowBattery());
            return false;
        }
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
        {
            Player.Instance.Coroutine(LowBattery());
            return false;
        }
    }

    public IEnumerator LowBattery()
    {
        batteryText.CrossFadeColor(Color.red, 0.3f, false, false);
        yield return new WaitForSeconds(0.3f);
        batteryText.CrossFadeColor(Color.white, 0.3f, false, false);
    }
}
