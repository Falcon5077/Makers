using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distant_Enemy : MonoBehaviour
{
    private Transform target;
    private bool attacking;
    private Vector2 attackAngle;
    [SerializeField]private float distance;
    [SerializeField]private float moveSpeed;
    [SerializeField]private bool shoot = true;
    public Transform bulletPoint;
    public GameObject bulletPrefab;
    public int bulletPower;
    float shootTime;
    HpSystem mHpSystem = new HpSystem();
    bool my_coroutine_is_running = false;
    // 발사 간격
    [SerializeField] float fireTime;
    // 발사해야할 시간
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        bulletFire();
        //Invoke("FollowTarget",0.5f); //? 이거 제대로 실행되지 않는거 같아요
    }

    private void FollowTarget() {
        if(Vector2.Distance(transform.position, target.position) > distance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
        }
    }
    
    //총알 발사 코드
    void bulletFire(){
        if (shoot){
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (shootTime <= Time.time){
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                bullet.GetComponent<Bullet>().bulletPower = bulletPower;
                shootTime = Time.time + fireTime;
            }
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
    
    //Enemy의 Circle Collider 안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 시 해당 코드내용 사용
    private void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = false로 세팅
        if(collision.tag == "bullet")
        {
            Debug.Log("hp : " + mHpSystem.m_HP);
            int p = collision.gameObject.GetComponent<Bullet>().bulletPower;
            if(mHpSystem.CalcHP(-p) <= 0)
            {
                Destroy(this.gameObject);
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            StartCoroutine("HitRoutine");
            
            Destroy(collision.gameObject);
        }

    }
}
