using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : Monster
{
    // 공격 사거리
    public float Range;
    // 총알 발사 위치
    public Transform bulletPoint;
    // 총알 프리펩
    public GameObject bulletPrefab;
    // 총알 데미지
    public int bulletPower;

    // 발사 딜레이
    public float shootTime;
    public float fireTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        bulletFire();
        FollowTarget();
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
}
