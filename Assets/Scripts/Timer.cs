using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Coins coins;

    public GameEconomy ge;
    public float timer;
    public float round;
    public Text timeDisplayer;
    void Awake()
    {
        ge.SetMaxSatisfaction(100);
        timer = 0;
        timeDisplayer.text = "Time: " + Mathf.Round(timer);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Round();
            timeDisplayer.text = "Time: " + Mathf.Round(timer);
        }
    }
     void Round(){
        timer += Time.deltaTime;
        if(timer > 5){            
            round++;
            timer = 0;
            ge.CalculateCosts();
            ge.CalculateSatisfaction();
            ge.DropOut();
            RoundEnd();
        }
    }
    void RoundEnd(){
        coins.spendMoney(ge.roundCost);
        ge.SetSatisfaction(ge.studentSatisfaction);
    }
}
