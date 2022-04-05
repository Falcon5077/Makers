using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    // static으로 선언된 PlayerMove
    // Player는 단 하나이므로 플레이어에 대한 참조가 필요할 경우 PlayerMove.Player 로 접근
    public static PlayerMove Player;

    // Player 이동속도
    public float playerMoveSpeed;

    //시간 * 스피드 움직임 값
    float moveX, moveY;
    
    void Awake() {
        // 초기화 작업
        if(Player == null)
            Player = this;
    }

    void Update(){
        moveX = Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * playerMoveSpeed * Time.deltaTime;

        transform.position = new Vector2(moveX + transform.position.x, moveY + transform.position.y);

    }
}
