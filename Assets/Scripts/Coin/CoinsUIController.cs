using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUIController : MonoBehaviour
{
    public Text coins;

    void Start()
    {
        RefreshText();
        CoinsManager.Instance.AddOnChangeCoinsListener(() => {
            RefreshText();
        });
    }

    private void RefreshText() {
        coins.text = "Coins: " + CoinsManager.Instance.Coins.ToString();
    }
}
