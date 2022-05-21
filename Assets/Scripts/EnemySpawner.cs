using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //적이 다 사라지면 스포너 죽는데 해결좀해줘양 해봤는디 난 못하겠다;; ㅠㅠ
    public GameObject Enemy;
    public GameObject DistanceEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",1f);
        
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity);          //근거리적 스폰
        Instantiate(DistanceEnemy,new Vector3(x,y,0),Quaternion.identity);  //원거리적 스폰

        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
