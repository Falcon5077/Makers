using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item item;
    // Start is called before the first frame update
    void Start()
    {
        if(item == null)
            item = this;
    }
    public void puzzleItem(Player p, GameObject c)
    {
        p.canMove = false;
        GameObject puzzle = c.gameObject.transform.GetChild(0).gameObject;
        puzzle.transform.parent = null;
        Camera.main.transform.position = new Vector3(0,0,-10);
        puzzle.SetActive(true);
        Time.timeScale = 0;
        Destroy(c.gameObject);  //아이템 삭제
    }
}
