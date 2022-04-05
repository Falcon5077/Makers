using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    // static으로 선언된 PlayerMove
    // Player는 단 하나이므로 플레이어에 대한 참조가 필요할 경우 PlayerMove.Player 로 접근
    public static PlayerMove Player;

    // Player 이동속도
    [SerializeField] private float playerMoveSpeed;

    //시간 * 스피드 움직임 계산
    float moveX, moveY;
    
    void Awake() {
        // 초기화 작업
        if(Player == null){
            Player = this;
        }
            
    }

    void Update(){
        //키보드 wasd 값 불러서 움직일 값 계산
        moveX = Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * playerMoveSpeed * Time.deltaTime;

        //플레이어 위치 값 재설정  
        transform.position += new Vector3(moveX , moveY);

    }
}
