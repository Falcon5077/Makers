using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI chatMsg;
    public string[] chatNames;
    public string[] chatContents;
    public int contentsNum;
    public int info = 0;
    public bool isWriting = false;
    string subText = null;
    // Start is called before the first frame update
    private void OnEnable() {
        //PlayerController.instance.CanMove = false;
    }
    public void ClickMsg()
    {
        if(isWriting)
            isWriting = false;
        else if(!isWriting)
        {
            NextChat();
        }
    }

    IEnumerator TypingAction(int n){
        isWriting = true;
        for(int i = 0; i< chatContents[n].Length; i++){

            yield return new WaitForSeconds(0.05f);

            subText += chatContents[n].Substring(1,i);
            chatMsg.text = subText;
            subText= "";

            if(!isWriting)
            {
                int length = chatContents[n].Length;
                chatMsg.text = chatContents[n].Substring(1,length-1);
                StopCoroutine("TypingAction");
            }
        }
    }

    public void reNewContents()
    {
        chaneName(0);

        int length = chatContents[0].Length;
        StartCoroutine("TypingAction",0);
    }

    public void NextChat(){
        contentsNum++;
        if(chatContents.Length == contentsNum){
            chatMsg.text = "";
            this.gameObject.SetActive(false);       
        }
        else 
        {
            int length = chatContents[contentsNum].Length;
            chaneName(contentsNum);
            StartCoroutine("TypingAction",contentsNum);
        }
    }

    public void chaneName(int i)
    {        
        info = int.Parse(chatContents[i].Substring(0,1));
        nameText.text = chatNames[info];
    }
}