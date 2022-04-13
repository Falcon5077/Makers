using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    [Header("추격 속도")]
    [SerializeField][Range(1f, 4f)] float moveSpeed = 3f;

    [Header("근접 거리")]
    [SerializeField][Range(0f, 3f)] float contactDistance = 1f;
    //bool follow = false;
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
        if(Vector2.Distance(transform.position, target.position) > contactDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else rb.velocity = Vector2.zero;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision) {   //Enemy의 Circle Collison내에 Player가 들어오게 되면 follow = true로 세팅
        follow = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {    //Enemy의 Circle Collison내에 Player가 없을 시 follow = false
        follow = false;
    }
    */
}
