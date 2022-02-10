using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoom : MonoBehaviour
{
    public Sprite[] rotations;
    public SavedInfo savedInfo;
    public SpriteRenderer sr;

    void Start()
    {
        savedInfo = GameObject.FindGameObjectWithTag("SavedInfo").GetComponent<SavedInfo>();
    }

    void Update()
    {
        if(transform.parent.gameObject.tag == "SavedInfo"){
            sr.enabled = true;
            sr.color = new Color(1, 1, 1, 0.5f);
            if(Input.GetKeyDown(KeyCode.Q)){
                transform.rotation *= Quaternion.Euler(0, 0, 90);
                savedInfo.rotationIndex++;
            }
            if(Input.GetKeyDown(KeyCode.E)){
                transform.rotation *= Quaternion.Euler(0, 0, -90);
                savedInfo.rotationIndex--;
            }

            if(savedInfo.rotationIndex > 3){
                savedInfo.rotationIndex = 0;
            }
            if(savedInfo.rotationIndex < 0){
                savedInfo.rotationIndex = 3;
            }

            sr.sprite = rotations[savedInfo.rotationIndex];
        }
    }
}
