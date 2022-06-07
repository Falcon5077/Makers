using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
    // 이름 UI Text
    public TextMeshProUGUI nameText;
    // 채팅 내용 UI Text
    public TextMeshProUGUI chatText;

    // 보여줄 이름 배열
    public string[] chatNames;
    // 보여줄 채팅 배열
    public string[] chatContents;

    // 채팅 인덱스
    public int contentsNum;

    // 채팅의 이름 인덱스
    public int nameNum = 0;

    // 현재 쓰고 있는지 체크하는 변수
    public bool isWriting = false;

    // 임시 저장 문자열
    string subText = null;

    private void Start() {
        transform.GetChild(0).gameObject.SetActive(true);
        instance = this;
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        //PlayerController.instance.CanMove = false;
    }

    // 클릭했을 때 호출되는 함수
    public void ClickMsg()
    {
        // 작성을 멈춤
        if(isWriting)
            isWriting = false;

        // 작성을 완료했다면 다음 채팅
        else if(!isWriting)
        {
            NextChat();
        }
    }

    // 채팅이 타이핑되는 연출 코루틴
    IEnumerator TypingAction(int n){
        isWriting = true;
        for(int i = 0; i< chatContents[n].Length; i++){

            yield return new WaitForSeconds(0.05f);

            // n번째 chatContents의 문자열을 첫번째부터 i번째까지 잘라서 출력
            subText += chatContents[n].Substring(1,i);
            chatText.text = subText;
            subText= "";

            // 작성을 완료했다면 연출을 멈추고 전채 메세지를 출력 .. 클릭을 통하여 출력이 중간에 중단됐을 경우를 대비
            if(!isWriting)
            {
                int length = chatContents[n].Length;
                chatText.text = chatContents[n].Substring(1,length-1);
                StopCoroutine("TypingAction");
            }
        }
    }

    // 새로운 채팅 갱신
    public void reNewContents()
    {
        changeName(0);

        int length = chatContents[0].Length;
        StartCoroutine("TypingAction",0);
    }

    // 다음 채팅 실행
    public void NextChat(){
        contentsNum++;
        if(chatContents.Length == contentsNum){
            chatText.text = "";
            this.gameObject.SetActive(false);       
        }
        else 
        {
            int length = chatContents[contentsNum].Length;
            changeName(contentsNum);
            StartCoroutine("TypingAction",contentsNum);
        }
    }

    // i번째 메세지의 화자로 이름 변경
    public void changeName(int i)
    {        
        nameNum = int.Parse(chatContents[i].Substring(0,1));
        nameText.text = chatNames[nameNum];
    }
}