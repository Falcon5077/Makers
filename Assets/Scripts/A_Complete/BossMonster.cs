using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : RangeMonster
{
    protected bool death_is_running = false;
    protected int bullet_Shooting_Count = 0;
    public GameObject Bomber_Mob;
    public GameObject Wall;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_localScale = transform.localScale;
        mHpSystem.m_HP = startHP;
        Wall = GameObject.Find("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        if(!death_is_running)
        {
            FollowTarget();
            RandomFire();
        }
    } 

    void RandomFire()
    {
        shootTime += Time.deltaTime;
        
        if (shootTime > fireTime){
            shootTime = 0;
            if(bullet_Shooting_Count++ % 5 == 0)
            {
                int r = Random.Range(0,7);
                int angle = Random.Range(-5, 5);
                Debug.Log(r);
                switch (r)
                {
                    case 0:
                        CustomBullet(angle);
                        CustomBullet(15 + angle);
                        CustomBullet(-15 + angle);
                        break;
                    case 1:
                        CustomBullet(angle);
                        CustomBullet(30 + angle);
                        CustomBullet(-30 + angle);
                        break;
                    case 2:
                        CustomBullet(angle);
                        CustomBullet(30 + angle);
                        CustomBullet(-30 + angle);
                        CustomBullet(15 + angle);
                        CustomBullet(-15 + angle);
                        break;
                    case 3:
                        CustomBullet(30 + angle);
                        CustomBullet(-30 + angle);
                        break;
                    case 4:
                        CustomBullet(15 + angle);
                        CustomBullet(-15 + angle);
                        break;
                    case 5:
                        Instantiate(Bomber_Mob, bulletPoint.position, Quaternion.identity);
                        break;
                }
            }
            else
                CustomBullet();
        }
    }

    void CustomBullet(int piv = 0)
    {
        if(Vector2.Distance(transform.position, target.position) > distance + Range) 
            return;

        Vector2 direction = target.position - bulletPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.AngleAxis(angle -90 -piv, Vector3.forward));
        bullet.GetComponent<Bullet>().bulletPower = bulletPower;
    }
    
    void bulletFire(){
        shootTime += Time.deltaTime;

        if(Vector2.Distance(transform.position, target.position) > distance + Range) 
            return;

        Vector2 direction = target.position - bulletPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (shootTime > fireTime){
            shootTime = 0;
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
            bullet.GetComponent<Bullet>().bulletPower = bulletPower;
        }
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
                bullet.GetComponent<Bullet>().bulletSpeed = 5;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0,0,(36*i) + (20 * j)));
            }
            yield return new WaitForSecondsRealtime(0.3f);
        }

        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;

        GameObject puzzle = PuzzleManager.instance.transform.GetChild(0).gameObject;
        puzzle.transform.position = Vector3.zero;
        Camera.main.transform.position = new Vector3(0,0,-10);
        puzzle.SetActive(true);
        
        Wall.SetActive(false);

        Destroy(this.gameObject);
    }

    void BossDeath()
    {
        death_is_running = true;
        Debug.Log("Boss death");

        GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);
        StartCoroutine("ScaleSmall");
        StartCoroutine("BossExplosion");
        //Time.timeScale = 0.5f;
    }
    
    override public void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = false로 세팅
        if(collision.tag == "bullet")
        {
            if(collision.GetComponent<Bullet>().isHit == true)
                return;

            collision.GetComponent<Bullet>().isHit = true;
            Destroy(collision.gameObject);
            
            Debug.Log("hp : " + mHpSystem.m_HP);
            int p = collision.gameObject.GetComponent<Bullet>().bulletPower;
            if(mHpSystem.CalcHP(-p) <= 0 && death_is_running == false)
            {
                BossDeath();
            }

            if(my_coroutine_is_running)
                StopCoroutine("HitRoutine");

            StartCoroutine("HitRoutine");
        }
    }
}