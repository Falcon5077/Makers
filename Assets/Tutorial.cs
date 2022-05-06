using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public bool isMove;
    public float mTime;
    private GameObject player;
    public TextMeshProUGUI str;

    public GameObject Enemy;
    public GameObject HealItem;
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    
    void CheckMove()
    {
        if(isMove)
            return;

        if(mTime >= 3f)
        {
            isMove = true;
            player.GetComponent<PlayerTuto>().shoot = true;
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
        yield return new WaitForSeconds(4f);
        str.text = "Enemies are created at random locations.";
        float x = Random.Range(-10,10);
        float y = Random.Range(-10,10);
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


        str.text = "If you kill an enemy, an item is created randomly.";
        x = player.transform.position.x + 3f;
        y = player.transform.position.y + 3f;
        GameObject d = Instantiate(HealItem,new Vector3(x,y,0),Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        Debug.Log(mTime);
    }
}
