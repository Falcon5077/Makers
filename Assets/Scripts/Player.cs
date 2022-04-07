using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    // static으로 선언된 player
    // player는 단 하나이므로 플레이어가 필요할 경우는 get, set을 이용해서 참조할 것  
    private static Player player;

    // Player 이동속도
    [SerializeField] private float playerMoveSpeed;
    // 시간 * 스피드 움직임 계산 x/y, hp
    private float moveX, moveY, playerHP = 100;
    // Get sprite
    SpriteRenderer heroSprite;
    // Get animation
    Animator anim;

    private void moveHeroSprite() {
        // 스프라이트 좌우 반전
        if (Input.GetKey("d") || Input.GetKey("a"))
        {
            heroSprite.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 스프라이트 애니메이션 적용
        if (Input.GetKey("d") || Input.GetKey("a"))
        {
            anim.SetBool("isSideWalk", true);
        }
        if (Input.GetKey("d") == false && Input.GetKey("a") == false)
        {
            anim.SetBool("isSideWalk", false);
        }
    }

    private void playerMove(){
        // 키보드 wasd 눌리는거 확인 후 움직일 값 계산
        moveX = Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * playerMoveSpeed * Time.deltaTime;

        // 플레이어 위치 값 재설정  
        transform.position += new Vector3(moveX, moveY);
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
        moveHeroSprite()
        playerMove();
    }
    
}
