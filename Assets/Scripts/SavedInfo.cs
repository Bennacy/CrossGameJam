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
    
    // [Space]
    // [Header("Booleans")]
    public bool mouseOverCanvas;
    public bool mouseOverPath;
    public bool canPlay;
    public bool clickedL;
    public bool dragging;
    public bool placingRoom;

    public Vector2 roomPos;
    public Vector3 mousePos;
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

    [Space]
    [Header("Grid Settings")]
    public int gridX;
    public int gridY;
    public Grid grid;

    void Awake()
    {
        if(self == null){
            self = this;
            rooms = new Room[100];
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        NewRoom();
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

    public void GetRoomsInLevel(){
        foreach(Transform child in grid.transform){
            Destroy(child.gameObject);
        }
        foreach(Transform child in grid.roomParent){
            Destroy(child.gameObject);
        }
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
}








