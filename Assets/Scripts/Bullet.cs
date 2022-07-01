using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    // 총알 살아있는 시간 
<<<<<<< Updated upstream:Assets/Scripts/Bullet.cs
    [SerializeField] float bulletLifeTime;
=======
    [SerializeField] public float bulletLifeTime;

>>>>>>> Stashed changes:Assets/Scripts/A_Complete/Bullet.cs
    // 총알 스피드
    [SerializeField] public float bulletSpeed;
    
    public bool isHit = false;
    public int bulletPower = 1;

    // 초기화 
    void Awake(){

    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start(){
        Destroy(gameObject, bulletLifeTime);
    }


    // Update is called once per frame
    void Update(){
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
}
