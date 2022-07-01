using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemy; // 0 근거리 1 원거리
    public GameObject Boss; //보스 오브젝트 선언
    public static EnemySpawner instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StartStage();
        }
    }

    public void StartStage()
    {
        Invoke("SpawnEnemy",1f);
    }

    void SpawnEnemy()
    {
        // 랜덤좌표에 적 생성
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        int MonsterType = Random.Range(0,Enemy.Count);
        Instantiate(Enemy[MonsterType],new Vector3(x,y,0),Quaternion.identity);

        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }

    public void StopSpawnEnemy()
    {
        CancelInvoke("SpawnEnemy");
        Debug.Log("몬스터 스폰 중지");
    }
    
    public void SpawnBoss()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        int MonsterType = Random.Range(0,2);
        GameObject boss = Instantiate(Boss,new Vector3(x,y,0),Quaternion.identity);
        boss.GetComponent<Monster>().mHpSystem.m_HP = 5;
        StopSpawnEnemy();
    }
}
