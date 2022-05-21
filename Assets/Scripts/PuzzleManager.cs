using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Player.player.canMove = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
