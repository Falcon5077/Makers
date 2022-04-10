using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour{
    // static으로 선언된 player
    // player는 단 하나이므로 플레이어가 필요할 경우는 get, set을 이용해서 참조할 것  
    private static Player player;

    // Player 이동속도
    [SerializeField] float playerMoveSpeed;
    // 캐릭터가 움직일 벡터
    Vector2 vector;
    // Get sprite
    SpriteRenderer playerSprite;
    // Get animation
    Animator anim;
    // player HP
    

    public Vector2 playerPosition
    {
        get { return player.GetComponent<Rigidbody2D>().position; }
        set { player.GetComponent<Rigidbody2D>().position = value; }
    }

    // 방향 enum
    enum Direction{
        Right, Left, Up, Down
    }

    // 방향 확인 함수 
    private bool checkDirectionBool(Direction dir){
        switch (dir){
            case Direction.Right:
                if(Input.GetAxisRaw("Horizontal") > 0){
                    return true;
                }
                return false;
            case Direction.Left:
                if (Input.GetAxisRaw("Horizontal") < 0){
                    return true;
                }
                return false;

            case Direction.Up:
                if (Input.GetAxisRaw("Vertical") > 0){
                    return true;
                }
                return false;
            case Direction.Down:
                if (Input.GetAxisRaw("Vertical") < 0){
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    private void moveHeroSprite() {
        // 스프라이트 좌우 반전, sideWalk로 변경 
        if (checkDirectionBool(Direction.Right)){
            playerSprite.flipX = false;
            anim.SetBool("isSideWalk", true);
        }
        if (checkDirectionBool(Direction.Left)){
            playerSprite.flipX = true;
            anim.SetBool("isSideWalk", true);
        }

        // Up 애니메이션 적용
        if (checkDirectionBool(Direction.Up)){
            anim.SetBool("isUpWalk", true);
        }
        else{
            anim.SetBool("isUpWalk", false);
        }

        // Down 애니메이션 적용  
        if (checkDirectionBool(Direction.Down)){
            anim.SetBool("isDownWalk", true);
        }
        else{
            anim.SetBool("isDownWalk", false);
        }

        // idle로 돌아가기 위해 확인 문 
        if ((checkDirectionBool(Direction.Right) == false && checkDirectionBool(Direction.Left) == false) ||
            (checkDirectionBool(Direction.Right) && checkDirectionBool(Direction.Left))){
            anim.SetBool("isSideWalk", false);
        }
        if (checkDirectionBool(Direction.Up) && checkDirectionBool(Direction.Down)){
            anim.SetBool("isUpWalk", false);
            anim.SetBool("isDownWalk", false);
        }
    }

    private void playerMove(){
        // 키보드 wasd 눌리는거 확인 후 움직일 값 계산
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");


        // 플레이어 위치 값 재설정
        player.GetComponent<Rigidbody2D>().velocity = vector * playerMoveSpeed;

    }


    void Awake(){
        // 초기화 작업
        if (player == null){
            player = this;
        }
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        moveHeroSprite();
        playerMove();
    }
    
}
