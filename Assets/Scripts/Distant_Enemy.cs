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
    float shootTime;
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
        }else transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed*Time.deltaTime);
    }
    
    //총알 발사 코드
    void bulletFire(){
        if (shoot){
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (shootTime <= Time.time){
                Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                shootTime = Time.time + fireTime;
            }
        }
        
    }
}
