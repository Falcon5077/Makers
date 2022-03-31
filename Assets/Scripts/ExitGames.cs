using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGames : MonoBehaviour
{
    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // PC에서는 ESC, 모바일에서는 뒤로가기
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick")) // 더블클릭 타이머 1초 ( 첫번째 클릭 후 1초내에 두번째 클릭 해야 더블클릭 판정)
                Invoke("DoubleClick", 1.0f);
       
        }
        else if (ClickCount == 2)   // 클릭 카운트가 2회 라면 (1초 내에 두번 연속 눌렀다면) EndGame 호출
        {
            CancelInvoke("DoubleClick");
            
            EndGame();
        }
  
    }
 
    void DoubleClick()
    {
        ClickCount = 0;
    }

    // 게임 종료 함수
    public void EndGame()
    {
        #if UNITY_EDITOR // 유니티 에디터라면 에디터 정지
            Debug.Log("EndGame");
            ClickCount = 0;
            UnityEditor.EditorApplication.isPaused = true;

        #else   // 에디터가 아니라면 (빌드 후) 애플리캐이션 종료
            Application.Quit();
        #endif
    }
}
