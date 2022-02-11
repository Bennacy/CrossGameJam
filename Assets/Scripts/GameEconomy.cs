using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEconomy : MonoBehaviour
{
    public Coins coins;

    public Slider slider;

    public Timer timer;

    public SavedInfo savedInfo;

    public int studentRent = 50;

    public int studentAmount = 20;

    public int floors;

    public int courses = 1;

    public float studentSatisfaction = 50;

    public float income;
    
    public float expenses;

    public float roundCost;
    
    public float maxStudents = 40;

    public void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            savedInfo.buildInfo[0].count += 1;
            Debug.Log("A classroom");
        }
    }
    public void CalculateCosts()
    {
        expenses = (savedInfo.buildInfo[10].maintenanceCost * savedInfo.buildInfo[10].count) + (savedInfo.buildInfo[9].maintenanceCost * savedInfo.buildInfo[9].count);
        income = (studentAmount * studentRent);
        roundCost = income - expenses;
    }
     public void SetMaxSatisfaction(float satisfaction){
        slider.maxValue = 100;}
    public void SetSatisfaction(float satisfaction){
        slider.maxValue = 100;
        slider.value = satisfaction;
        if(satisfaction > 100) satisfaction = 100;
        Debug.Log(studentSatisfaction);
    }
    public float CalculateSatisfaction(){
       SatisfactionCalculator(0, 20);
       SatisfactionCalculator(1, 100);
       SatisfactionCalculator(2, 300);
       SatisfactionCalculator(3, 25);
       SatisfactionCalculator(4, 25);
       SatisfactionCalculator(5, 50);
       SatisfactionCalculator(7, 500);
       SatisfactionCalculator(8, 700);
       SatisfactionCalculator(9, 20);
       SatisfactionCalculator(10, 15);
       return studentSatisfaction;
    }
    public float SatisfactionCalculator(int noB, int perstudent){
        int satisfactor=studentAmount/savedInfo.buildInfo[noB].count;
        if(perstudent==satisfactor){
            return studentSatisfaction;
        }
        else if(perstudent>satisfactor){
            return studentSatisfaction -(perstudent/satisfactor);
        }
        else if(perstudent<satisfactor){
            return studentSatisfaction + (perstudent/satisfactor);
        }
        else return studentSatisfaction;
    }
    public float DropOut(){
       studentSatisfaction = ((studentSatisfaction/100) * (studentAmount / 5));
       return studentSatisfaction;
    }

    public float BuyCourses(){
        courses +=1;
        maxStudents = 40 * courses;
        coins.spendMoney(800);
        return courses;
    }
}
