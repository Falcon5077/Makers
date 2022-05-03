using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public float distance = 10;
    public Vector3 fisrtPosition;
    public Vector3 endPosition;
    public bool isCorrect = false;
    private void Awake() {
        fisrtPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp() {
        if(Vector3.Distance(transform.position,endPosition) < 0.5f)
        {
            transform.position = endPosition;
            isCorrect = true;
        }
        else{
            transform.position = fisrtPosition;
            isCorrect = false;
        }

        PuzzleManager.instance.CheckPuzzleClear();
            
    }
    
}
