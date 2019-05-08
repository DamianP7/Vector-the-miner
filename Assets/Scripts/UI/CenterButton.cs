using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterButton : MonoBehaviour
{
    #region Instance
    private static CenterButton instance;
    public static CenterButton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CenterButton>();
                if (instance == null)
                    Debug.LogError("No CenterButton found on the scene!");
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] Text text;
    [SerializeField] Button button;

    public void RemoveAllActions()
    {
        button.onClick.RemoveAllListeners();
        text.text = "";
    }

    public void AddAction(VoidDelegate function, string textToShow)
    {
        button.onClick.AddListener(() => function());
        text.text = textToShow;
    }
}

public delegate void VoidDelegate();