using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SquareState{occupied, free}

public class Grid : MonoBehaviour
{
    [Space]
    [Header("References")]
    public GameObject refTile;
    private SavedInfo savedInfo;
    public Transform roomParent;
    [Space]
    
    private int rows;
    private int cols;
    private float tileSize = 1;



    // Start is called before the first frame update
    void Start()
    {
        savedInfo = GameObject.FindGameObjectWithTag("SavedInfo").GetComponent<SavedInfo>();
        cols = savedInfo.gridX;
        rows = savedInfo.gridY;
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.x);
    }

    public void GenerateGrid(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }

        int halfX = (int)Mathf.Floor(cols / 2);
        int halfY = (int)Mathf.Floor(rows / 2);

        for (int col = 0; col < cols; col++)
        {
            GameObject currCol = new GameObject(col.ToString());
            currCol.transform.parent = transform;
            
            float posX = col * tileSize;
            posX = posX - (halfX * tileSize);
            currCol.transform.localPosition = new Vector3(posX, 0);
            currCol.transform.localScale = new Vector2(1, 1);
            for (int row = 0; row < rows; row++)
            {
                GameObject tile = (GameObject)Instantiate(refTile, currCol.transform);
                tile.name = (currCol.name + ", " + row.ToString());

                float posY = row * tileSize;
                posY = posY - (halfY * tileSize);
                tile.transform.localScale = new Vector2(tileSize, tileSize);
                tile.transform.localPosition = new Vector2(0, posY);

                Square squareScript = tile.gameObject.GetComponent<Square>();
                squareScript.col = col;
                squareScript.row = row;
                squareScript.state = SquareState.free;
            }
        }
    }

    public bool PlaceRoom(int initX, int initY, Vector2 size, int roomIndex, int rotationIndex, bool isNew){
        for(int x = initX; x < (initX + size.x); x++){
            Transform currCol = transform.Find(x.ToString());
            for(int y = initY; y > initY - size.y; y--){
                Transform currSquare = currCol.Find(currCol.name.ToString() + ", " + y.ToString());
                Square script = currSquare.gameObject.GetComponent<Square>();
                if(script.state == SquareState.occupied){
                    Debug.Log("NOOOO!");
                    return false;
                }              
            }
        }
        if(rotationIndex % 2 != 0){
            Vector2 tempVector = size;
            size.x = tempVector.y;
            size.y = tempVector.x;
        }
        Transform firstCol = transform.Find(initX.ToString());
        Transform firstSquare = firstCol.Find(firstCol.name + ", " + (initY).ToString());
        GameObject newRoom = Instantiate(savedInfo.roomPrefabs[roomIndex]);
        newRoom.transform.position = new Vector2(firstSquare.position.x - .5f, firstSquare.localPosition.y + .5f);
        newRoom.transform.parent = roomParent;
        newRoom.GetComponent<SpriteRenderer>().sprite = newRoom.GetComponent<RotateRoom>().rotations[rotationIndex];
        newRoom.transform.rotation = Quaternion.Euler(0, 0, rotationIndex*90);
        if(isNew){
            Vector2Int gridPos = new Vector2Int(initX, initY);
            savedInfo.AddRoom(newRoom.transform.position, gridPos, rotationIndex);
        }
        for(int x = initX; x < (initX + size.x); x++){
            Transform currCol = transform.Find(x.ToString());
            for(int y = initY; y > initY - size.y; y--){
                Transform currSquare = currCol.Find(currCol.name.ToString() + ", " + y.ToString());
                Square script = currSquare.gameObject.GetComponent<Square>();
                script.state = SquareState.occupied;             
            }
        }
        return true;
    }
}