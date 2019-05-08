using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : Building
{
    private void Awake()
    {
        buildingAction = LoadBattery;
    }

    void LoadBattery()
    {
        Player.Instance.stats.Battery += 1000;
    }
}
