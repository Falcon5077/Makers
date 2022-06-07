using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 킬한 적의 카운트 세는 변수
    public int killEnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckKillEnemyCount();
    }

    void CheckKillEnemyCount()
    {
        // 20마리 이상 킬한 경우 보스 스폰
        if(killEnemyCount == 20)
        {
            EnemySpawner.instance.SpawnBoss();
            killEnemyCount++;
        }
    }
}
