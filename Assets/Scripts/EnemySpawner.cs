using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //적이 다 사라지면 스포너 죽는데 해결좀해줘양 해봤는디 난 못하겠다;; ㅠㅠ
    public GameObject[] Enemy; // 0 근거리 1 원거리
    public GameObject Boss; //보스 오브젝트 선언
    public static EnemySpawner instance;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",1f);
        
        if(instance == null)
        {
            instance = this;
        }
        
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        int MonsterType = Random.Range(0,2);
        Instantiate(Enemy[MonsterType],new Vector3(x,y,0),Quaternion.identity);

        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }
    
    public void SpawnBoss()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        int MonsterType = Random.Range(0,2);
        GameObject boss = Instantiate(Boss,new Vector3(x,y,0),Quaternion.identity);
        boss.GetComponent<Distant_Enemy>().mHpSystem.m_HP = 50;
    }
}
