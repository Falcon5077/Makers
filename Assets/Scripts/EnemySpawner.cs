using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",1f);
        
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
        Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity);

        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
