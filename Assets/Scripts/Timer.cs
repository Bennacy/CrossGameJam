using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Coins coins;
    public float timer;
    public float round;
    public float roundCosts = 90;
    public Text timeDisplayer;
    void Awake()
    {
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
            RoundEnd();
        }
    }
    void RoundEnd(){
        coins.spendMoney(roundCosts);
    }
}
