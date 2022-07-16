using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public GameObject[] puzzle;
    public GameObject Wall;
    public GameObject NextScene;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        Wall = GameObject.Find("Wall");
    }

    public void CheckPuzzleClear()
    {
        for(int i = 0; i < puzzle.Length; i++)
        {
            if(puzzle[i].GetComponent<PuzzleTile>().isCorrect == false)
                return;
        }

        // 모든 퍼즐이 Correct라면 퍼즐 통과!

        Debug.Log("Clear");
        Time.timeScale = 1;
        gameObject.SetActive(false);

        // 퍼즐 클리어 후 동작
        Wall.SetActive(true);
        GameObject temp = Instantiate(NextScene);
        temp.GetComponent<chatData>().ChatStart();
        
        GameObject temp2 = GameObject.Find("Player");
        temp2.GetComponent<Player>().shoot = false;
        //SceneManager.LoadScene("Game 2");   //퍼즐 맞으면 Game 2 씬으로 전환 / 퍼즐이 더 필요하면 해당 퍼즐 종료후 넘어가도록 수정하세요
    }

}
