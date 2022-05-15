using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public List<GameObject> EnemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",1f);
        
    }

    async void SpawnEnemy()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            if(EnemyList[i] == null)
                EnemyList.RemoveAt(i);

            Debug.Log("Remove");
        }
        if(EnemyList.Count <= 4)
        {
            float x = Random.Range(-10,10);
            float y = Random.Range(-10,10);
            
            EnemyList.Add(Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity));
        }
        else
            Debug.Log(EnemyList.Count + "It's Full");
        
        Invoke("SpawnEnemy",Random.Range(1.5f,3f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
