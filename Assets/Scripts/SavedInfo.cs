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

[Serializable]
public class Room{
    public int level;
    public int index;
    public GameObject roomObject;
    public Vector2 position;
    public Vector2Int gridPosition;
    public int rotationIndex;
}

[Serializable]
public class BuildingInfo{
    public string name;
    public int buildCost;
    public int maintenanceCost;
    public int count;
}

public class SavedInfo : MonoBehaviour
{
    public static SavedInfo self;
    // public int level;
    // [Space]
    // [Header("References")]
    public string currScene;
    public int currLevel;
    public int roomIndex;
    public int newRoomIndex;
    public int placedRooms;
    public GameObject[] roomPrefabs;
    public Vector2[] roomSizes;
    public Sprite[] roomSprites;
    public Room[] rooms;
    public GameObject roomPreview;
    public int rotationIndex;
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

    public BuildingInfo[] buildInfo;
    
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
    // private TilemapGrid tGrid;
    public Grid grid;
    

    public Coins coins;
    public float timer;
    public float round;
    public float roundCosts = 90;
    public Text timeDisplayer;

    
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

    void Start()
    {
        if(self == null){
            self = this;
            rooms = new Room[100];
            buildInfo = new BuildingInfo[10];
            InitializeInfo();            
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
        NewRoom();
        timer = 0;
        timeDisplayer.text = "Time: " + Mathf.Round(timer);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            currLevel = Mathf.Clamp(currLevel - 1, 0, 10);
            currScene = "";
        }if(Input.GetKeyDown(KeyCode.UpArrow)){
            currLevel = Mathf.Clamp(currLevel + 1, 0, 10);
            currScene = "";
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
        if(roomIndex != newRoomIndex){
            NewRoom();
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(placingRoom){
            roomPreview.SetActive(true);
            roomPreview.transform.position = roomPos;
        }else{
            roomPreview.SetActive(false);
        }
    }

    public void NewRoom(){
        rotationIndex = 0;
        roomIndex = newRoomIndex;
        Destroy(roomPreview);
        roomPreview = Instantiate(roomPrefabs[roomIndex]);
        roomPreview.transform.parent = transform;
    }

    public void NewScene(){
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currScene = SceneManager.GetActiveScene().name;
        GetRoomsInLevel();
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
        grid.GenerateGrid();
        placedRooms = 0;
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].roomObject != null){
                placedRooms++;
                if(rooms[i].level == currLevel){
                    int index = rooms[i].index;
                    grid.PlaceRoom(rooms[i].gridPosition.x, rooms[i].gridPosition.y, roomSizes[index], rooms[i].index, rooms[i].rotationIndex, false);
                }
            }
        }
    }

    public void AddRoom(Vector2 positionToAdd, Vector2Int gridPositionToAdd, int rotationIndexToAdd){
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].roomObject == null){
                rooms[i].level = currLevel;
                rooms[i].index = roomIndex;
                rooms[i].roomObject = roomPrefabs[roomIndex];
                rooms[i].position = positionToAdd;
                rooms[i].gridPosition = gridPositionToAdd;
                rooms[i].rotationIndex = rotationIndexToAdd;
                placedRooms++;
                return;
            }
        }
    }

    private void InitializeInfo(){
            buildInfo[0].name = "BasicRoom";
            buildInfo[0].buildCost = 100;
            buildInfo[0].maintenanceCost = 50;
            buildInfo[0].count = 0;
            
            buildInfo[1].name = "LoungeBar";
            buildInfo[1].buildCost = 100;
            buildInfo[1].maintenanceCost = 50;
            buildInfo[1].count = 0;
            
            buildInfo[2].name = "BasicLounge";
            buildInfo[2].buildCost = 100;
            buildInfo[2].maintenanceCost = 50;
            buildInfo[2].count = 0;
            
            buildInfo[3].name = "Stairs";
            buildInfo[3].buildCost = 100;
            buildInfo[3].maintenanceCost = 50;
            buildInfo[3].count = 0;
            
            buildInfo[4].name = "Elevator";
            buildInfo[4].buildCost = 100;
            buildInfo[4].maintenanceCost = 50;
            buildInfo[4].count = 0;
            
            buildInfo[5].name = "Bathroom";
            buildInfo[5].buildCost = 100;
            buildInfo[5].maintenanceCost = 50;
            buildInfo[5].count = 0;
            
            buildInfo[6].name = "Corridor";
            buildInfo[6].buildCost = 100;
            buildInfo[6].maintenanceCost = 0;
            buildInfo[6].count = 0;
            
            buildInfo[7].name = "Reception";
            buildInfo[7].buildCost = 100;
            buildInfo[7].maintenanceCost = 50;
            buildInfo[7].count = 0;
            
            buildInfo[8].name = "Library";
            buildInfo[8].buildCost = 100;
            buildInfo[8].maintenanceCost = 50;
            buildInfo[8].count = 0;
            
            buildInfo[9].name = "Professors";
            buildInfo[9].buildCost = 100;
            buildInfo[9].maintenanceCost = 50;
            buildInfo[9].count = 0;
            
            buildInfo[10].name = "Staff";
            buildInfo[10].buildCost = 100;
            buildInfo[10].maintenanceCost = 50;
            buildInfo[10].count = 0;
    }
}








