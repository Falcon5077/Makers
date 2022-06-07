using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// RectTransform 이동을 위한 좌표(from, to)와 속도 speed 를 구조체로 만듬
public struct FromTo
{
    public Vector3 from;
    public Vector3 to;
    public float speed;
    public RectTransform tra;

    public FromTo(Vector3 from,Vector3 to,float speed,RectTransform tra)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
        this.tra = tra;
    }
}
public class FadeInOut : MonoBehaviour
{
    public RectTransform TopPannel;
    public RectTransform DownPannel;
    public GameObject BlurPannel;
    public bool isBlur = false;

    // from, to, speed, TopPannel 정보를 FromTo에 저장하여 코루틴 실행

    // FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,0,0),1f,TopPannel);
    // StartCoroutine("MoveFromTo",TopPannelWay);
    IEnumerator MoveFromTo(FromTo f)
    {
        var t = 0f;

        // t가 1이 될 때 까지 반복
        while (t < 1f)
        {
            // 그러나 t는 speed가 곱해진 deltaTime
            t += f.speed * Time.unscaledDeltaTime;

            // anchoredPosition을 from에서 to로 선형보간
            f.tra.anchoredPosition3D = Vector3.Lerp(f.from, f.to, t);
            yield return null;
        }

        Debug.Log("MoveEnd");
    }

    IEnumerator BlurFadeIn()
    {
        // 시간을 멈추고 isBlur를 true로
        Time.timeScale = 0;
        isBlur = true;

        for(int i = 1; i <= 10; i++)
        {
            if(i == 4)
            {   
                // 탑판넬과 바텀판넬을 이동 시킴
                FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,0,0),1f,TopPannel);
                FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,540,0),1f,DownPannel);
                StartCoroutine("MoveFromTo",TopPannelWay);
                StartCoroutine("MoveFromTo",BottomPannelWay);   
            }
            // 셰이더 블러 값을 i를 통해 증가
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // 블러처리가 끝났으니 isBlur를 false로
        isBlur = false;
    }

    IEnumerator BlurFadeOut()
    {
        // isBlur를 true로
        isBlur = true;

        //탑판넬과 바텀판넬을 이동 시킴
        FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,540,0),1f,TopPannel);
        FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,0,0),1f,DownPannel);
        StartCoroutine("MoveFromTo",TopPannelWay);
        StartCoroutine("MoveFromTo",BottomPannelWay);
        
        for(int i = 10; i > 0; i--)
        {
            // 셰이더 블러 값을 i를 통해 증가
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // 블러처리가 끝났으니 isBlur를 false로, timeScale을 1로
        isBlur = false;


        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!isBlur)
                StartCoroutine("BlurFadeIn");
            else Debug.Log("Already Working");
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(!isBlur)
                StartCoroutine("BlurFadeOut");
            else Debug.Log("Already Working");
        }
    }
}
