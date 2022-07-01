using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public bool isMove;
    public float mTime;
    public GameObject player;
    public TextMeshProUGUI str;

    public GameObject Enemy;
    public GameObject HealItem;
    public GameObject BulletSpItem;

    void Awake()
    {
        player = GameObject.Find("Player");
        player.GetComponent<Player>().shoot = false;
    }

    private void Start() 
    {
        //EnemySpawner.instance.StartStage();
        //FadeInOut.instance.StartProd();
    }
    
    void CheckMove()
    {
        if(isMove)
            return;

        if(mTime >= 3f)
        {
            isMove = true;
            player.GetComponent<Player>().shoot = true;
            str.text = "The bullet is fired in the direction of the mouse.";
            
            StartCoroutine("SpawnEnemy");
        }

        if(Input.GetKey(KeyCode.W))
        {
            mTime += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            mTime += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            mTime += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            mTime += Time.deltaTime;
        }
    }
    
    IEnumerator SpawnEnemy()
    {
         //bullet Speed 아이템 표시
        str.text = "When you get a bullet-shaped item, the bullet speeds up for a while.";
        float x = player.transform.position.x + 3f;
        float y = player.transform.position.y + 3f;
        GameObject e = Instantiate(BulletSpItem,new Vector3(x, y, 0), Quaternion.identity);
        while(e != null) 
        {
            yield return new WaitForSeconds(1f);
        }

        //적 3개 출력하고 플레이어가 모두 죽일때 까지 대기
        //yield return new WaitForSeconds(4f);
        str.text = "Enemies are created at random locations.";
        x = Random.Range(-10,10);
        y = Random.Range(-10,10);
        GameObject a = Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity);

         x = Random.Range(-10,10);
         y = Random.Range(-10,10);
        GameObject b = Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity);

         x = Random.Range(-10,10);
         y = Random.Range(-10,10);
        GameObject c = Instantiate(Enemy,new Vector3(x,y,0),Quaternion.identity);

        while(a != null || b != null || c != null)
        {
            yield return new WaitForSeconds(1f);
        }

        //대기 종료 후 회복아이템 표시
        str.text = "If you kill an enemy, an item is created randomly.";
        x = player.transform.position.x + 3f;
        y = player.transform.position.y + 3f;
        GameObject d = Instantiate(HealItem,new Vector3(x,y,0),Quaternion.identity);

        while(d != null)    //플레이어가 회복아이템 먹을때 가지 대기.
        {
            yield return new WaitForSeconds(1f);
        }

        str.text = "Well done! Now you can play to 1 Stage.";
        yield return new WaitForSeconds(3f);
        str.text = "Good Luck...";
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        //CheckMove();
        Debug.Log(mTime);
    }
}
