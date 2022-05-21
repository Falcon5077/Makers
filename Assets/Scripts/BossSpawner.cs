using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    //여기가 보스 스폰하는 스크립트인데 이걸 puzzlemanager에서 맞추면 불러와서 보스 스폰하도록 하면 될거 같은데 못하겠다 ㅠㅠ
    public GameObject Boss;         //보스 오브젝트 선언
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",1f);
    }


    public void SpawnEnemy()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        Instantiate(Boss,new Vector3(x,y,0),Quaternion.identity);   //보스 스폰 

        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
