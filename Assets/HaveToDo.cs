using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HaveToDo : MonoBehaviour
{
    public GameObject enemy;
    public GameObject NextSceneItem;
    public bool isTutorial;
    public bool canMoveNextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnRangedEnemy()
    {
        Instantiate(enemy);
    }

    public void MoveEnemy()
    {
        GetComponent<RangeMonster>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy() {
        if(isTutorial)
        {
            chatData temp = new chatData();
            temp.names = new string[]{"???","주인공"};
            temp.names[0] = "???";
            temp.contents = new string[]{"0크아아아악","1저 녀석이 온 곳으로 가보자"};
            temp.endRoutine = "";
            temp.ChatStart();
            canMoveNextScene = true;
            Instantiate(NextSceneItem);
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game 1");
    }
}
