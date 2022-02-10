using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if(Input.GetMouseButtonUp(0) && !savedInfo.dragging && !savedInfo.mouseOverCanvas && !savedInfo.mouseOverPath){
                Clicked();
            }
        }
    }

    private void Clicked(){
        Debug.Log("A");
        // if(transform.childCount == 0){
        //     if(savedInfo.buildingTower){
        //         if(savedInfo.Pay(savedInfo.newTowerCost)){
        //             if(!Input.GetKey(KeyCode.LeftShift)){
        //                 savedInfo.buildingTower = false;
        //             }
        //             AssignTower(savedInfo.towerToBuild);
        //         }else{
        //             savedInfo.buildingTower = false;
        //         }
        //     }
        // }else{
        //     towerScript.rangeOverlay.gameObject.SetActive(true);
        //     savedInfo.upgrading = true;
        // }
        // if(state == SquareState.free){
        //     state = SquareState.occupied;
        // }else{
        //     state = SquareState.free;
        // }

        gridScript.PlaceRoom(col, row, 10, 5);
    }
}