using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveToDo : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Door;
    public GameObject Ending_1;
    public GameObject Ending_2;
    public bool SpawnDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStageOne()
    {
        EnemySpawner.instance.SpawnEnemy();
    }

    public void Timer()
    {
        GameObject temp = Instantiate(Enemy);
        temp.GetComponent<chatData>().ChatStart();
    }

    public void FirstEnding()
    {
        GameObject temp = GameObject.Find("Player");
        GameObject temp2 = GameObject.Find("Grid");
        temp.GetComponent<Animator>().enabled = false;
        temp.GetComponent<Player>().shoot = false;
        temp2.GetComponent<BackgroundScrolling>().enabled = false;
        GameObject temp3 = Instantiate(Ending_2);
        //temp.GetComponent<
    }

    public void RestartGame()
    {
        FadeInOut.instance.GameEnd();
    }

    public void SecondEnding()
    {

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

    public void PhaseTwoStart()
    {
        GetComponent<BossMonster>().Shoot = true;
        GetComponent<BossMonster>().distance = 50;
        Enemy.GetComponent<Player>().shoot = true;
        GetComponent<BossPattern>().enabled = true;
    }
}
