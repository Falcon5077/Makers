using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    // 총알 살아있는 시간 
    [SerializeField] public float bulletLifeTime;

    // 총알 스피드
    [SerializeField] public float bulletSpeed;
    
    // 총알이 무언가에 닿았는가?
    public bool isHit = false;

    // 총알의 데미지
    public int bulletPower = 1;

    // 초기화 
    void Awake(){
        if(GetComponent<AudioSource>() != null) 
            GetComponent<AudioSource>().volume = SoundPlayer.EFM_value;
    }

    // Start is called before the first frame update
    void Start(){
        // 발사로부터 LifeTime 후 삭제
        Destroy(gameObject, bulletLifeTime);
    }


    // Update is called once per frame
    void Update(){
        // 발사 방향으로 전진
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
}
