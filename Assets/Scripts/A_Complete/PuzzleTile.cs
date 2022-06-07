using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public float zPosition = 10;

    // 시작 위치
    public Vector3 firstPosition;

    // 정답 위치
    public Vector3 endPosition;

    // 맞았는지 체크하는 변수
    public bool isCorrect = false;
    private void Awake() {
        firstPosition = transform.position;
    }

    void OnMouseDrag()
    {
        // 오브젝트를 드래그하여 이동
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, zPosition);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp() {
        // 마우스를 땠을 때 정답 위치에 놓여지면 
        if(Vector3.Distance(transform.position,endPosition) < 0.5f)
        {
            // 이 퍼즐 조각은 Correct 처리
            transform.position = endPosition;
            isCorrect = true;
        }
        // 아니라면
        else{
            // 이 퍼즐 조각은 처음 위치로
            transform.position = firstPosition;
            isCorrect = false;
        }

        // 퍼즐 조각을 놓을 때 마다 퍼즐 클리어했는지 체크
        PuzzleManager.instance.CheckPuzzleClear();
    }
    
}
