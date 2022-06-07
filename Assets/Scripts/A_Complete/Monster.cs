using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Transform target;
    [SerializeField]float moveSpeed = 3;
    public HpSystem mHpSystem = new HpSystem();
    protected bool my_coroutine_is_running = false;
    public int power = 1;
    [SerializeField]protected float distance = 0;
    protected Vector3 m_localScale;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_localScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    protected void FollowTarget() {
        if(target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-m_localScale.x,m_localScale.y,0);
        }
        else
        {
            transform.localScale = new Vector3(m_localScale.x,m_localScale.y,0);
        }

        if(Vector2.Distance(transform.position, target.position) > distance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
        }
    }

    // Hit했을 때 이미지 빨갛게 연출
    IEnumerator HitRoutine()
    {
        my_coroutine_is_running = true;

        GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
        int sort = GetComponent<SpriteRenderer>().sortingOrder;
        GetComponent<SpriteRenderer>().sortingOrder++;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        GetComponent<SpriteRenderer>().sortingOrder = sort;

        my_coroutine_is_running = false;
    }

    //Enemy의 Circle Collider 안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 시 해당 코드내용 사용
    virtual public void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = false로 세팅
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

