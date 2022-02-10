using UnityEngine;
 
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    public float zoomSpeed;
    public float maxZoom;
    public float minZoom;
    public float dragTolerance = .2f;
    private Vector3 dragOrigin;
    public Vector3[] previousPos;
    public bool canDrag;
    private Vector3 startDrag;
    private bool zoomed;


    void Start()
    {
        previousPos = new Vector3[3];
    }

    void LateUpdate()
    {
        PanCamera();
        if(Input.mouseScrollDelta.y != 0){
            Zoom(-(int)Input.mouseScrollDelta.y);
        }
        if(Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)){
            canDrag = false;
        }
    }

    private void PanCamera(){
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)){
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            startDrag = transform.position;
            canDrag = true;
        }

        if(Input.GetMouseButton(1) || Input.GetMouseButton(2)){
            if(canDrag){
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
                previousPos[0] = previousPos[1];
                previousPos[1] = previousPos[2];
                previousPos[2] = difference;

                cam.transform.position += difference;

                Vector3 draggedDist = transform.position - startDrag;
                if(Vector3.Magnitude(draggedDist) >= dragTolerance){
                    
                }
            }
        }else{
            canDrag = false;
        }
    }

    private void Zoom(int dir){
        float newSize = cam.orthographicSize + zoomSpeed * dir;
        cam.orthographicSize = Mathf.Clamp(newSize, maxZoom, minZoom);
    }
}