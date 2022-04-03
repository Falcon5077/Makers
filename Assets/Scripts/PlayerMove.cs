using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // static으로 선언된 PlayerMove
    // Player는 단 하나이므로 플레이어에 대한 참조가 필요할 경우 PlayerMove.Player 로 접근
    public static PlayerMove Player;

    // 조이스틱 참조
    public Joystick mJoystick;

    // Player 이동속도
    public float playerMoveSpeed;
    
    void Awake()
    {
        // 초기화 작업
        if(Player == null)
            Player = this;
        if(mJoystick == null)
            mJoystick = GameObject.Find("Floating Joystick").GetComponent<Joystick>();
    }

    void Update()
    {
        // 플레이어 이동
        if(mJoystick != null){
            transform.Translate(new Vector3(mJoystick.Horizontal,mJoystick.Vertical,0) * playerMoveSpeed * Time.deltaTime);
            Debug.Log(mJoystick.Horizontal + ", " + mJoystick.Vertical);
        }
    }
}
