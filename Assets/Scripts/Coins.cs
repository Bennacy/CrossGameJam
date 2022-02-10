using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Coins : MonoBehaviour
{
    public float coins;
    public Text coinDisplayer;

    void Awake()
    {
        coins = 20000;
        coinDisplayer.text = "Coins: " + coins.ToString();
    }

    private void Update()
    {
        CoinsCheat();
        UpdateCoins();
    }

    public void CoinsCheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            getMoney(100);
            coinDisplayer.text = "Coins: " + coins;
        }
    }

    public void UpdateCoins()
    {

      coinDisplayer.text = "Coins: " + coins;

    }

    public void spendMoney(float cost){
        coins -= cost;
    } 

    public void getMoney(float stonks){
        coins += stonks;
    }
}

