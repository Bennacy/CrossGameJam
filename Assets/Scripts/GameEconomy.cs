using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEconomy : MonoBehaviour
{
    public Coins coins;

    public Slider slider;

    public Timer timer;
    public float staffCost = 50;

    public float staffAmount = 50;

    public float teacherCost = 100;

    public float teacherAmount = 100;

    public float studentRent = 50;

    public float studentAmount = 500;

    public float floors;

    public float courses = 1;

    public float studentSatisfaction = 50;

    public float income;
    
    public float expenses;

    public float roundCost;

    // Start is called before the first frame update
    public void CalculateCosts()
    {
        expenses = (staffCost * staffAmount) + (teacherAmount * teacherCost);
        income = (studentAmount * studentRent);
        roundCost = income - expenses;
    }
     public void SetMaxSatisfaction(float satisfaction){
        slider.maxValue = 100;}
    public void SetSatisfaction(float satisfaction){
        slider.maxValue = 100;
        slider.value = satisfaction;
        if(satisfaction > 100) satisfaction = 100;
    }
    public float CalculateSatisfaction(){
       return studentSatisfaction += 2;
        
    }
}
