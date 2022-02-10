using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SquareState{occupied, free}
public class Square : MonoBehaviour
{    
    [Space]
    [Header("References")]
    public Grid gridScript;
    private SpriteRenderer sr;
    private SavedInfo savedInfo;
    public LayerMask clickLayer;
    public GameObject towerPrefab;
    public SquareState state;
    public GameObject assignedRoom;

    [Space]

    [Space]
    [Header("TileInfo")]
    public int col;
    public int row;
    private bool mouseOver;
    private RaycastHit2D ray;
    

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gridScript = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        savedInfo = GameObject.FindGameObjectWithTag("SavedInfo").GetComponent<SavedInfo>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, clickLayer);
        if(ray.collider != null){
            mouseOver = ray.collider.name == gameObject.name;
        }

        if(state == SquareState.free){
            sr.color = new Color(1,1,1);
        }else{
            sr.color = new Color(1,0,0);
        }

        if(mouseOver){
            if(savedInfo.placingRoom){
                savedInfo.roomPos = transform.position;
                savedInfo.roomPos.x -= transform.localScale.x / 2;
                savedInfo.roomPos.y += transform.localScale.y / 2;
            }
            if(Input.GetMouseButtonUp(0) && !savedInfo.dragging && !savedInfo.mouseOverCanvas && !savedInfo.mouseOverPath){
                Clicked();
            }
        }
    }

    private void Clicked(){
        if(savedInfo.placedRooms < savedInfo.rooms.Length){
            gridScript.PlaceRoom(col, row, savedInfo.roomSizes[savedInfo.roomIndex], true);
        }
    }
}