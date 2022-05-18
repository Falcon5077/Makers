using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public GameObject[] puzzle;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CheckPuzzleClear()
    {
        for(int i = 0; i < puzzle.Length; i++)
        {
            if(puzzle[i].GetComponent<Puzzle>().isCorrect == false)
                return;
        }

        Debug.Log("Clear");
        Time.timeScale = 1;
        gameObject.SetActive(false);
        //SceneManager.LoadScene("Game 2");   //퍼즐 맞으면 Game 2 씬으로 전환 / 퍼즐이 더 필요하면 해당 퍼즐 종료후 넘어가도록 수정하세요
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
