using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnergyCosts : ScriptableObject
{
    public List<ActionCost> actionCosts;

    [System.Serializable]
    public class ActionCost
    {
        public Action action;
        public int cost;
    }
}
