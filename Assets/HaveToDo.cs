using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveToDo : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Door;
    public bool SpawnDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Timer()
    {
        GameObject temp = Instantiate(Enemy);
        temp.GetComponent<chatData>().ChatStart();

    }

    public void SpawnTutoEnemy()
    {
        Invoke("Timer",1f);
    }

    public void MoveEnemy()
    {
        GetComponent<RangeMonster>().enabled = true;
        SpawnDoor = true;
    }

    private void OnDestroy() {
        if(SpawnDoor){
            GameObject temp = Instantiate(Door);
            temp.GetComponent<chatData>().ChatStart();
        }
            
    }
}
