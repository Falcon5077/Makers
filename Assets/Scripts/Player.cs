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
    SpriteRenderer heroSprite;
    // Get animation
    Animator anim;

    private void moveHeroSprite() {
        // 스프라이트 좌우 반전, sideWalk로 변경 
        if (Input.GetKey("d") || Input.GetKey("a")){
            heroSprite.flipX = Input.GetAxisRaw("Horizontal") == -1;
            anim.SetBool("isSideWalk", true);
        }

        if (Input.GetKey("w")){
            anim.SetBool("isUpWalk", true);
        }
        else{
            anim.SetBool("isUpWalk", false);
        }

        if (Input.GetKey("s")){
            anim.SetBool("isDownWalk", true);
        }
        else{
            anim.SetBool("isDownWalk", false);
        }
       
        if ((Input.GetKey("d") == false && Input.GetKey("a") == false) ||
            (Input.GetKey("d") && Input.GetKey("a"))){
            anim.SetBool("isSideWalk", false);
        }
        if ((Input.GetKey("w") && Input.GetKey("s"))){
            anim.SetBool("isUpWalk", false);
            anim.SetBool("isDownWalk", false);
        }
    }

    private void playerMove(){
        // 키보드 wasd 눌리는거 확인 후 움직일 값 계산
        if (Input.GetKey("d")){
            vector.x = 1;
        }
        else if(Input.GetKey("a")){
            vector.x = -1;
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("a") || (Input.GetKey("d") && Input.GetKey("a"))
            || (Input.GetKey("d") == false && Input.GetKey("a") == false)){
            vector.x = 0;
        }

        if (Input.GetKey("w")){
            vector.y = 1;
        }
        else if (Input.GetKey("s")){
            vector.y = -1;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || (Input.GetKey("w") && Input.GetKey("s"))
            || (Input.GetKey("w") == false && Input.GetKey("s") == false)){
            vector.y = 0;
        }


        // 플레이어 위치 값 재설정){
        player.GetComponent<Rigidbody2D>().velocity = vector * playerMoveSpeed;

    }


    void Awake(){
        // 초기화 작업
        if (player == null){
            player = this;
        }
        heroSprite = GetComponent<SpriteRenderer>();
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
