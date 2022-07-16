using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class chatData : MonoBehaviour
{
    public bool chatStart = false;
    [Header("각 인덱스에 사용될 이름을 저장")]
    public string[] names;
    [Header("0안녕하세요(주인공 : 안녕하세요) 처럼\nnames의 인덱스 + 대화내용")]
    public string[] contents;
    [SerializeField]
    public static int index = 0;
    public static bool isChat = false;
    public string endRoutine;
    void Start()
    {
        Invoke("CheckStart",1f);
    }

    public void CheckStart()
    {
        if(chatStart == true)
        {
            ChatStart();
        }
    }
    private void Update() {
        // 대화 시작 매커니즘 코딩해야함
        if(Input.GetKeyDown(KeyCode.X))
        {
            ChatStart();
        }
    }

    public void ChatStart()
    {
        if (ChatManager.instance != null)
            ChatManager.instance.gameObject.SetActive(true);
        isChat = true;
        ChatManager.instance.chatNames = names;
        ChatManager.instance.chatContents = contents;
        ChatManager.instance.contentsNum = 0;
        ChatManager.instance.reNewContents();
        ChatManager.instance.lastChatData = this;
    }

    public void EndChat()
    {
        if(endRoutine != "")
            SendMessage(endRoutine);

        isChat = false;
        Destroy(this);
    }
}