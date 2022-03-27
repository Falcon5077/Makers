using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGames : MonoBehaviour
{
    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);
       
        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            
            EndGame();
        }
  
    }
 
    void DoubleClick()
    {
        ClickCount = 0;
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
            Debug.Log("EndGame");
            ClickCount = 0;
            UnityEditor.EditorApplication.isPaused = true;

        #else
            Application.Quit();
        #endif
    }
}
