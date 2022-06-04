using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    IEnumerator MoveFromTo(FromTo f)
    {
        var t = 0f;
    
        while (t < 1f)
        {
            t += f.speed * Time.unscaledDeltaTime;
            f.tra.anchoredPosition3D = Vector3.Lerp(f.from, f.to, t);
            yield return null;
        }

        Debug.Log("MoveEnd");
    }
    IEnumerator BlurFadeIn()
    {
        Time.timeScale = 0;
        isBlur = true;
        for(int i = 1; i <= 10; i++)
        {
            if(i == 4)
            {   
                FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,0,0),1f,TopPannel);
                FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,540,0),1f,DownPannel);
                StartCoroutine("MoveFromTo",TopPannelWay);
                StartCoroutine("MoveFromTo",BottomPannelWay);   
            }
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        isBlur = false;
    }


    IEnumerator BlurFadeOut()
    {
        isBlur = true;
        FromTo TopPannelWay = new FromTo(TopPannel.anchoredPosition,new Vector3(0,540,0),1f,TopPannel);
        FromTo BottomPannelWay = new FromTo(DownPannel.anchoredPosition,new Vector3(0,0,0),1f,DownPannel);
        StartCoroutine("MoveFromTo",TopPannelWay);
        StartCoroutine("MoveFromTo",BottomPannelWay);
        for(int i = 10; i > 0; i--)
        {
            BlurPannel.GetComponent<Image>().material.SetInt("_Radius",i);
            yield return new WaitForSecondsRealtime(0.1f);
        }
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
