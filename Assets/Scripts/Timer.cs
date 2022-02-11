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
        ge.SetMaxSatisfaction();
        timer = 0;
        timeDisplayer.text = "Time: " + Mathf.Round(timer);
        ge.maxStudents = 40;
        ge.studentAmount = 20;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Round();
            //timeDisplayer.text = "Time: " + Mathf.Round(timer);
            Debug.Log(ge.studentAmount);
            Debug.Log(round);
        }
    }
     void Round(){            
            round++;
            timer = 0;
            ge.CalculateCosts();
            ge.CalculateSatisfaction();
            coins.spendMoney(ge.roundCost);
            ge.SetSatisfaction(ge.studentSatisfaction);
            ge.StudentApplications();
    }
}
