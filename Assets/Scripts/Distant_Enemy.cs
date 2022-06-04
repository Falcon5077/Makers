using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distant_Enemy : MonoBehaviour
{
    private Transform target;
    private bool attacking;
    private Vector2 attackAngle;
    [SerializeField]private float distance;
    [SerializeField]private float distanceOut;
    [SerializeField]private float moveSpeed;
    [SerializeField]private bool shoot = true;
    public Transform bulletPoint;
    public GameObject bulletPrefab;
    public SpriteRenderer animRenderer;
    public int bulletPower;
    float shootTime;
    public HpSystem mHpSystem = new HpSystem();
    bool my_coroutine_is_running = false;
    float range;
    // 발사 간격
    [SerializeField] float fireTime;
    private Vector3 m_localScale;
    // 발사해야할 시간
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_localScale = transform.localScale;
        animRenderer = GetComponent<SpriteRenderer>();
        if(transform.tag == "Boss")
            bulletPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        bulletFire();
        //Invoke("FollowTarget",0.5f); //? 이거 제대로 실행되지 않는거 같아요
    }

    private void FollowTarget() {
        if(target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-m_localScale.x,m_localScale.y,0);
        }
        else
        {
            transform.localScale = new Vector3(m_localScale.x,m_localScale.y,0);
        }
        if(Vector2.Distance(transform.position, target.position) > distance + range) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
        }
        else
        {
            range = Random.Range(-1.0f,1.0f);
        }
    }
    
    //총알 발사 코드
    void bulletFire(){
        if(Vector2.Distance(transform.position, target.position) > distanceOut + range) 
            return;

        if (shoot){
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (shootTime <= Time.time){
                GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                bullet.GetComponent<Bullet>().bulletPower = bulletPower;
                shootTime = Time.time + fireTime;
            }
        }
        
    }
    IEnumerator HitRoutine()
    {
        my_coroutine_is_running = true;
        animRenderer.color = new Color(1,0,0,1);
        int sort = animRenderer.sortingOrder;
        animRenderer.sortingOrder++;
        yield return new WaitForSeconds(0.1f);        
        animRenderer.color = new Color(1,1,1,1);
        my_coroutine_is_running = false;
        animRenderer.sortingOrder = sort;
    }

    IEnumerator ScaleSmall()
    {
        for(int i = 100; i > 0; i--){
            transform.localScale = new Vector3(0.0025f * i,0.0025f * i,0.0025f * i);
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

    IEnumerator BossExplosion()
    {
        for(int j = 0; j < 5; j++){
            for(int i = 0; i < 10; i++){
                GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
                bullet.GetComponent<Bullet>().bulletPower = 0;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,(36*i) + (20 * j)));
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    IEnumerator DeathRoutine()
    {
        if(transform.tag == "Boss")
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);
            StartCoroutine("ScaleSmall");
            StartCoroutine("BossExplosion");
            Time.timeScale = 0.5f;
            yield return new WaitForSecondsRealtime(2f);
            Time.timeScale = 1f;

            GameObject puzzle = PuzzleManager.instance.transform.GetChild(0).gameObject;
            puzzle.transform.position = Vector3.zero;
            Camera.main.transform.position = new Vector3(0,0,-10);
            puzzle.SetActive(true);
            Time.timeScale = 0;
        }        
        
        yield return new WaitForSecondsRealtime(0.01f);
        if(GameManager.instance != null)
            GameManager.instance.killEnemyCount++;
        Destroy(this.gameObject);
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
            if(mHpSystem.CalcHP(-p) == 0) // 버그 날 수도 있음 ==을 <=로
            {
                StartCoroutine("DeathRoutine");
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            if(mHpSystem.m_HP >= 0)
                StartCoroutine("HitRoutine");
            
        }

    }
}

