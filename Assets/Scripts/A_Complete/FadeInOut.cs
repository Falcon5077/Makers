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
<<<<<<< HEAD
=======
    public FadeInOut instance;
>>>>>>> Boss
    public RectTransform TopPannel;
    public RectTransform DownPannel;
    public GameObject BlurPannel;
    public bool isBlur = false;
<<<<<<< HEAD
=======
    public chatData[] chatData_instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
>>>>>>> Boss

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

<<<<<<< HEAD
    IEnumerator BlurFadeIn()
=======
    IEnumerator BlurFadeIn(float T)
>>>>>>> Boss
    {
        // 시간을 멈추고 isBlur를 true로
        Time.timeScale = 0;
        isBlur = true;

        for(int i = 1; i <= 10; i++)
        {
            if(i == 4)
            {   
                // 탑판넬과 바텀판넬을 이동 시킴
<<<<<<< HEAD
                FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,0,0),1f,TopPannel);
                FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,540,0),1f,DownPannel);
=======
                FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,0,0),T,TopPannel);
                FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,540,0),T,DownPannel);
>>>>>>> Boss
                StartCoroutine("MoveFromTo",TopPannelWay);
                StartCoroutine("MoveFromTo",BottomPannelWay);   
            }
            // 셰이더 블러 값을 i를 통해 증가
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
<<<<<<< HEAD
            yield return new WaitForSecondsRealtime(0.1f);
=======
            yield return new WaitForSecondsRealtime(0.1f/T);
>>>>>>> Boss
        }

        // 블러처리가 끝났으니 isBlur를 false로
        isBlur = false;
    }

<<<<<<< HEAD
    IEnumerator BlurFadeOut()
=======
    IEnumerator BlurFadeOut(float T)
>>>>>>> Boss
    {
        // isBlur를 true로
        isBlur = true;

        //탑판넬과 바텀판넬을 이동 시킴
<<<<<<< HEAD
        FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,540,0),1f,TopPannel);
        FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,0,0),1f,DownPannel);
=======
        FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,540,0),T,TopPannel);
        FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,0,0),T,DownPannel);
>>>>>>> Boss
        StartCoroutine("MoveFromTo",TopPannelWay);
        StartCoroutine("MoveFromTo",BottomPannelWay);
        
        for(int i = 10; i > 0; i--)
        {
            // 셰이더 블러 값을 i를 통해 증가
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
<<<<<<< HEAD
            yield return new WaitForSecondsRealtime(0.1f);
=======
            yield return new WaitForSecondsRealtime(0.1f / T);
>>>>>>> Boss
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
<<<<<<< HEAD
                StartCoroutine("BlurFadeIn");
=======
                StartCoroutine("BlurFadeIn",4);
>>>>>>> Boss
            else Debug.Log("Already Working");
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(!isBlur)
<<<<<<< HEAD
                StartCoroutine("BlurFadeOut");
            else Debug.Log("Already Working");
        }
=======
                StartCoroutine("BlurFadeOut",4);
            else Debug.Log("Already Working");
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine("Production");
            StartCoroutine("Production");
        }
    }

    public void StartProd()
    {

    }

    int count = 0;

    IEnumerator Production()
    {
        StartCoroutine("BlurFadeIn", 1.5f);
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine("BlurFadeOut", 1.5f);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine("BlurFadeIn", 4);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine("BlurFadeOut", 4);
        yield return new WaitForSecondsRealtime(1f);

        if(count++ % 2 == 1){
            Debug.Log(chatData.index);
            chatData_instance[chatData.index++].ChatStart();
        }
>>>>>>> Boss
    }
}
