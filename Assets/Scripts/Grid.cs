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
    [Space]
    
    private int rows;
    private int cols;
    private float tileSize = 1;



    // Start is called before the first frame update
    void Start()
    {
        savedInfo = GameObject.FindGameObjectWithTag("SavedInfo").GetComponent<SavedInfo>();
        cols = savedInfo.gridY;
        rows = savedInfo.gridX;
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.x);
    }

    private void GenerateGrid(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }

        int halfX = (int)Mathf.Floor(cols / 2);
        int halfY = (int)Mathf.Floor(rows / 2);

        for (int row = 0; row < rows; row++)
        {
            GameObject currRow = new GameObject(row.ToString());
            currRow.transform.parent = transform;
            
            float posX = row * tileSize;
            posX = posX - (halfX * tileSize);
            currRow.transform.localPosition = new Vector3(posX, 0);
            currRow.transform.localScale = new Vector2(1, 1);
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(refTile, currRow.transform);
                tile.name = (currRow.name + ", " + col.ToString());

                float posY = col * tileSize;
                posY = posY - (halfY * tileSize);
                tile.transform.localScale = new Vector2(tileSize, tileSize);
                tile.transform.localPosition = new Vector2(0, posY);

                Square squareScript = tile.gameObject.GetComponent<Square>();
                squareScript.row = row;
                squareScript.col = col;
                squareScript.state = SquareState.free;
            }
        }
    }
}