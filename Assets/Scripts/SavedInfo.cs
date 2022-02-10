using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SavedInfo : MonoBehaviour
{
    public static SavedInfo self;
    // public int level;
    // [Space]
    // [Header("References")]
    public string currScene;
    // public Square pressedTile;
    // public LayerMask canvas;
    // public LayerMask path;
    // private RaycastHit2D canvasRay;
    // private RaycastHit2D pathRay;
    private Camera cam;
    // public GameObject towerToBuild;
    // public TextAsset waves;
    // public TextAsset scores;
    // [Space]
    
    // [Space]
    // [Header("Booleans")]
    public bool mouseOverCanvas;
    public bool mouseOverPath;
    public bool canPlay;
    public bool clickedL;
    public bool dragging;
    public bool buildingTower;
    public bool upgrading;
    public bool inALevel;
    // [Space]

    // [Space]
    // [Header("Health")]
    // public int currHealth;
    // public int maxHealth;
    // [Space]

    // [Space]
    // [Header("Global Info")]
    // public int totalLevels;
    // public LevelTime[] levelScores;

    // [Serializable]
    // public class LevelTime{
    //     public int minutes;
    //     public int seconds;
    // }
    // [Space]

    // [Space]
    // [Header("Waves")]
    // public Vector3Int[] spawnDir;
    // public string[] levelStrings;
    // public int currWave;
    // public Wave[] waveArray;
    // public float[] spawnInterval;
    // public float[] waveInterval;

    // [Serializable]
    // public class Wave{
    //     public int[] wave;
    // }

    // [Space]
    // [Header("Currency / Time")]
    public int money;
    // public int basicCost;
    // public int plasmaCost;
    // public int iceCost;
    // public int missileCost;
    // public int newTowerCost;
    // public float time;
    // [Space]

    // [Space]
    [Header("Grid Settings")]
    public int gridX;
    public int gridY;
    private Grid grid;
    // private TilemapGrid tGrid;

    void Start()
    {
        if(self == null){
            self = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // string waveText = waves.text;
        // totalLevels = CountCharInString(ref waveText, '/');

        // levelStrings = new string[totalLevels];
        // for(int i = 0; i < totalLevels; i++){
        //     levelStrings[i] = CutStringAt(ref waveText, '/');
        // }

        // spawnDir = new Vector3Int[totalLevels];
        // spawnDir[0] = spawnDir[1] = spawnDir[3] = Vector3Int.up;
        // spawnDir[2] = Vector3Int.right;

        // NewScene();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if(currScene != SceneManager.GetActiveScene().name){
            NewScene();
        }
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // canvasRay = Physics2D.Raycast(mousePos, Vector3.zero, 0, canvas);

        // if(inALevel){
        //     pathRay = Physics2D.Raycast(mousePos, Vector3.zero, 0, path);

        //     if(canvasRay.collider != null){
        //         mouseOverCanvas = true;
        //     }else{
        //         mouseOverCanvas = false;
        //     }
        //     if(pathRay.collider != null){
        //         mouseOverPath = true;
        //     }else{
        //         mouseOverPath = false;
        //     }
        //     TilemapUpdate();
        //     GridUpdate();

        //     time += Time.deltaTime;

        //     if(currHealth <= 0){
        //         BackToMain();
        //     }
        // }
    }

    public void NewScene(){
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currScene = SceneManager.GetActiveScene().name;

        // string scoreReference = scores.text;
        // int scoresSaved = CountCharInString(ref scoreReference, '/');
        // levelScores = new LevelTime[scoresSaved];

        // for(int i = 0; i < scoresSaved; i++){
        //     levelScores[i] = new LevelTime();
        //     CutStringAt(ref scoreReference, '-');
        //     levelScores[i].minutes = (int)GetValueBefore(ref scoreReference, ':');
        //     levelScores[i].seconds = int.Parse(CutStringAt(ref scoreReference, '/'));
        // }

        // if(GameObject.FindGameObjectWithTag("TileGrid") != null){
        //     tGrid = GameObject.FindGameObjectWithTag("TileGrid").GetComponent<TilemapGrid>();
        // }
        // if(GameObject.FindGameObjectWithTag("Grid") != null){
        //     grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        // }        currHealth = maxHealth;

        // inALevel = currScene.Contains("Lvl");
        // string levelName = currScene;

        // if(inALevel){
        //     level = (int)GetValueBefore(ref levelName, 'L');

        //     CreateWaves();
        // }
        
        // canPlay = true;
    }

    public void AlterMoney(int change){
        money += change;
        if(money < 0){
            money = 0;
        }
    }

    public bool Pay(int cost){
        if(money - cost >= 0){
            money -= cost;
            return true;
        }else{
            return false;
        }
    }
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
    /* This Upate is the one that made it work!
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Round();
            timeDisplayer.text = "Time: " + Mathf.Round(timer);
        }
    }*/
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
