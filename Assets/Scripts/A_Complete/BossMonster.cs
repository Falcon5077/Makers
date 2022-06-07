using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : RangeMonster
{

    protected bool death_is_running = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!death_is_running)
            FollowTarget();
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
            yield return new WaitForSecondsRealtime(0.3f);
        }

        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;

        GameObject puzzle = PuzzleManager.instance.transform.GetChild(0).gameObject;
        puzzle.transform.position = Vector3.zero;
        Camera.main.transform.position = new Vector3(0,0,-10);
        puzzle.SetActive(true);

        Destroy(this.gameObject);
    }

    void BossDeath()
    {
        death_is_running = true;
        Debug.Log("Boss death");

        GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);
        StartCoroutine("ScaleSmall");
        StartCoroutine("BossExplosion");
        Time.timeScale = 0.5f;
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
