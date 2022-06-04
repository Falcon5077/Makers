using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    //private Rigidbody2D rb;
    private Transform target;
    [SerializeField]float moveSpeed;
    
    HpSystem mHpSystem = new HpSystem();
    bool my_coroutine_is_running = false;

    //float contactDistance = 1.85f; // Player 감지거리 (현재 사용중이 아니라 주석처리함)
    bool follow = true; // Player가 Collider내에 들어왔는지 체크를 위한 bool변수 (false: 아님, true: 들어옴)
    public int power = 1;
    public int bulletPower = 0;
    // Start is called before the first frame update
    void Start()
    {
        follow = true;
        //rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget() {
        /*
        Enemy의 Circle Collider안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 때 아래의 코드 대신 해당 코드 사용
        if(Vector2.Distance(transform.position, target.position) > contactDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else rb.velocity = Vector2.zero;
        */
        if(follow){
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    // 
    IEnumerator HitRoutine()
    {
        my_coroutine_is_running = true;
        GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
        int sort = GetComponent<SpriteRenderer>().sortingOrder;
        GetComponent<SpriteRenderer>().sortingOrder++;
        yield return new WaitForSeconds(0.1f);        
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        my_coroutine_is_running = false;
        GetComponent<SpriteRenderer>().sortingOrder = sort;
    }

    //Enemy의 Circle Collider 안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 시 해당 코드내용 사용
    private void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = false로 세팅
        if(collision.tag == "bullet")
        {
            if(collision.GetComponent<Bullet>().isHit == true)
                return;

            collision.GetComponent<Bullet>().isHit = true;
            Destroy(collision.gameObject);
            
            Debug.Log("hp : " + mHpSystem.m_HP);
            int p = collision.gameObject.GetComponent<Bullet>().bulletPower;
            if(mHpSystem.CalcHP(-p) <= 0)
            {
                if(GameManager.instance != null)
                    GameManager.instance.killEnemyCount++;
                Destroy(this.gameObject);
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            StartCoroutine("HitRoutine");
            
        }

    }
    
}
