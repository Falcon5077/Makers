using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using TMPro;

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
    double hp = 150.0;
    // 발사할 총알 저장
    public GameObject bulletPrefab;
    public GameObject BulletSpeedItem;
    // 총알 발사 여부 
    public bool shoot = true;
    // 발사 간격
    [SerializeField] float fireTime;
    // 발사해야할 시간
    float shootTime;
    // bullet position
    public Transform bulletPoint;
    bool my_coroutine_is_running = false;
    HpSystem mHpSystem = new HpSystem();
    public TextMeshProUGUI HPText;
    bool isSpeed = false;
    int bulletCount = 0;

    public List<GameObject> HP_List;
    public GameObject HP_point;
    public Transform HP_Head;

    // bullet power
    [SerializeField] int bulletPower = 1;
    public int power{
        get { return  bulletPower; }
        set { bulletPower = value; }
    }


    public Vector2 playerPosition{
        get { return  transform.position; }
        set { transform.position = value; }
    }

    public double playerHP{
        get { return hp; }
        set { hp = value; }
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

    void bulletFire(){
        if (shoot){
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 아래 두줄은 나중에 총에 추가하시면 총이 마우스 커서 위치에 따라 360도 회전할거에요 
            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = rotation;

            if (shootTime <= Time.time){
                GameObject b = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                b.GetComponent<Bullet>().bulletPower = power;
                shootTime = Time.time + fireTime;
                //추가된내용
                if(isSpeed) {
                    bulletCount++;
                    if(bulletCount > 30) {      //총알 속도 아이템 효과 상실 후
                        fireTime = 0.5f;
                        isSpeed = false;
                        bulletCount = 0;
                        float x = player.transform.position.x + 3f;
                        float y = player.transform.position.y + 3f;
                        Instantiate(BulletSpeedItem,new Vector3(x, y, 0), Quaternion.identity); //총알속도 아이템 다시 띄움
                    }
                }
            }
        }
        
    }

    void Awake(){
        // 초기화 작업
        if (player == null){
            player = this;
        }

        SetHpUI();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        shootTime = fireTime;
        shoot = true;
    }

    // Update is called once per frame
    void Update(){
        moveHeroSprite();
        playerMove();
        bulletFire();

    }

    void SetHpUI()
    {
        for(int i = 0; i < HP_List.Count; i++)
        {
            Destroy(HP_List[i]);
        }

        for(int i = 0; i < mHpSystem.m_HP; i++)
        {
            HP_List.Add(Instantiate(HP_point,HP_Head));
        }
    }
    
    IEnumerator HitRoutine()
    {
        my_coroutine_is_running = true;

        GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
        yield return new WaitForSeconds(0.1f);        
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);

        my_coroutine_is_running = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = false로 세팅
        if(collision.tag == "monster")
        {
            int p = collision.gameObject.GetComponent<Monster>().power;

            if(mHpSystem.CalcHP(-p) <= 0)
            {
                Debug.Log("사망");
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            StartCoroutine("HitRoutine");

            Destroy(collision.gameObject);
        }        
        
        if (collision.tag == "enemyBullet") {
            int p = collision.gameObject.GetComponent<Bullet>().bulletPower;

            if(mHpSystem.CalcHP(-p) <= 0)
            {
                Debug.Log("사망");
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            StartCoroutine("HitRoutine");

            Destroy(collision.gameObject);
        }

        if (collision.tag == "heal") {
            mHpSystem.CalcHP(5);           //HP아이템 습득 시 추가 5
            Debug.Log("HP: " + mHpSystem.m_HP);
            Destroy(collision.gameObject);  //아이템 삭제
        }

        if (collision.tag == "puzzle") {
            GameObject puzzle = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject;
            puzzle.transform.parent = null;
            puzzle.transform.position = Vector3.zero;
            Camera.main.transform.position = new Vector3(0,0,-10);
            puzzle.SetActive(true);
            Time.timeScale = 0;
            Destroy(collision.gameObject);  //아이템 삭제
        }

        //추가된 내용
        if (collision.tag == "BulletSpeedItem")
        {
            fireTime = 0.1f;
            isSpeed = true;
            Destroy(collision.gameObject);
        }

        SetHpUI();
    }
}
