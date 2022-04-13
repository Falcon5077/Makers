using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distant_Enemy : MonoBehaviour
{
    private Transform target;
    [SerializeField]private float distance;
    [SerializeField]private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget() {
        if(Vector2.Distance(transform.position, target.position) > distance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
        }else transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed*Time.deltaTime);
    }
}
