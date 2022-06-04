using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatInfor : MonoBehaviour
{
    public ChatManager chatUI;
    public string[] names;
    public string[] contents;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(chatUI != null)
                chatUI.gameObject.SetActive(true);
            chatUI.chatNames = names;
            chatUI.chatContents = contents;
            chatUI.contentsNum = 0;
            chatUI.reNewContents();
        }
    }
}