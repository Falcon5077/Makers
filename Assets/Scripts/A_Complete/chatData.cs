using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class chatData : MonoBehaviour
{
    [Header("각 인덱스에 사용될 이름을 저장")]
    public string[] names;
    [Header("0안녕하세요(주인공 : 안녕하세요) 처럼\nnames의 인덱스 + 대화내용")]
    public string[] contents;
    private void Update() {
        // 대화 시작 매커니즘 코딩해야함
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(ChatManager.instance != null)
                ChatManager.instance.gameObject.SetActive(true);
            ChatManager.instance.chatNames = names;
            ChatManager.instance.chatContents = contents;
            ChatManager.instance.contentsNum = 0;
            ChatManager.instance.reNewContents();
        }
    }
}