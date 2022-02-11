using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEconomy : MonoBehaviour
{
    public Coins coins;

    public Slider slider;

    public Timer timer;


    public int studentRent = 50;

    public float studentAmount = 20;

    public int floors;

    public int courses = 1;

    public float studentSatisfaction = 0;

    public float income;
    
    public float expenses;

    public float roundCost;
    
    public float maxStudents = 40;

    public void Update(){
    }
    public void CalculateCosts()
    {
        expenses = 400;
        income = (studentAmount * studentRent);
        roundCost = expenses - income;
    }
     public void SetMaxSatisfaction(){
        slider.maxValue = 100;
        slider.minValue = -100;
        }
    public void SetSatisfaction(float satisfaction){
        satisfaction = studentSatisfaction;
        Mathf.Clamp(studentSatisfaction, 0, 100);
    }
    public void CalculateSatisfaction(){
       SatisfactionCalculator(0);//20);  // at 20 students +0  at 41 -1
       SatisfactionCalculator(0);//100); // at 20 students +1  at 41 +1
       SatisfactionCalculator(0);//300); //  at 20 students +1  at 41 +1
       SatisfactionCalculator(0);//25); //  at 20 students +1  at 41 -1
       SatisfactionCalculator(0);//25); //  at 20 students +1  at 41 -1
       SatisfactionCalculator(0);//50); //  at 20 students +1  at 41 +1
       SatisfactionCalculator(0);//500); //  at 20 students +1  at 41 +1
       SatisfactionCalculator(0); //  at 20 students +1  at 41 +1
       SatisfactionCalculator(0);//4); //  at 20 students -1  at 41 -1
       SatisfactionCalculator(0);//2); //  at 20 students -1  at 41 -1
    }
    public float SatisfactionCalculator(/*int noB,*/ int perstudent){

        float satisfactor=studentAmount;//noB would be the "divisor" here, the bottom part of the fraction, the number of buildings built
        if(perstudent==satisfactor){
            return studentSatisfaction;
        }
        else if(perstudent>satisfactor){

            return studentSatisfaction +=1;    
        }
        else if(perstudent<satisfactor){
            
            return studentSatisfaction -=1;
        }
        else return studentSatisfaction;
    }

    public void BuyCourses(){
        courses +=1;
        maxStudents = 40 * courses;
        coins.spendMoney(800);
    }

    public void BuyBuildings(){

    }
    public float StudentApplications(){
        Mathf.Clamp(studentAmount,0,maxStudents);
        StudentApplicationsCalculator();
        return studentAmount;
        
    }
    public float StudentApplicationsCalculator(){
       return studentAmount += (studentSatisfaction/5);
    }
}
