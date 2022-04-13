using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    [SerializeField]float moveSpeed;

    //float contactDistance = 1f; // Player 감지거리
    //bool follow = false; // Player가 Collider내에 들어왔는지 체크를 위한 bool변수 (false: 아님, true: 들어옴)
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget() {
        /*
        Enemy의 Circle Collider안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 때 아래의 코드 대신 해당 코드 사용
        if(Vector2.Distance(transform.position, target.position) > contactDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else rb.velocity = Vector2.zero;
        */

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
    /*
    Enemy의 Circle Collider 안에 들어왔을 시 이동하는 조건으로 작동시키고 싶을 시 해당 코드내용 사용
    private void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = true로 세팅
        follow = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {    //Enemy의 Circle Collison내에 Player가 없을 시 follow = false
        follow = false;
    }
    */
}
