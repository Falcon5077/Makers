using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{    
    Vector3 pos; //현재위치
    public float delta = 2.0f; // 좌(우)로 이동가능한 (x)최대값
    public float speed = 3.0f; // 이동속도

    public bool PatterStart = true;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(GetComponent<Monster>().distance != 50){
        if(PatterStart){
            Vector3 v = pos;
            v.y += delta * Mathf.Sin(Time.time * speed);
            // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.
            transform.position = v;
        }
    }

}
