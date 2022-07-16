using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    // 배경음악과 효과음
    public AudioSource bgm;
    public AudioSource efm;

    // 배경음악과 효과음 슬라이더
    public Slider[] BGM_Slider;
    public Slider[] EFM_Slider;

    // 배경음악과 효과음 볼륨
    public float backVolume;
    public float effectVolume;

    // 배경음악과 효과음 토글
    public Toggle BGM_Toggle;
    public Toggle EFM_Toggle;

    // 배경음악과 효과음 상수
    private const int AudioType_BGM = 1;
    private const int AudioType_EFM = 2;
    // Start is called before the first frame update
    void Start()
    {
        // 배경음악과 효과음에 대해 저장된 정보 읽어오기
        backVolume = PlayerPrefs.GetFloat("BGM",1);
        effectVolume = PlayerPrefs.GetFloat("EFM",1);
        
        SoundPlayer.EFM_value = effectVolume;

        bgm.volume = backVolume;
        efm.volume = effectVolume;

        BGM_Slider[0].value = backVolume;
        EFM_Slider[0].value = effectVolume;

        if(backVolume == 0)
            BGM_Toggle.isOn = false;
        if(effectVolume == 0)
            EFM_Toggle.isOn = false;

        Debug.Log(backVolume + "," + effectVolume);
        transform.parent.gameObject.SetActive(false);
    }

    // 볼륨 변경 함수
    public void VolumeChange(int type)
    {
        if(type == AudioType_BGM)
        {
            bgm.volume = BGM_Slider[0].value;
            backVolume = BGM_Slider[0].value;

            if(bgm.volume == 0)
                BGM_Toggle.isOn = false;
            else
                BGM_Toggle.isOn = true;
        }
        if(type == AudioType_EFM)
        {
            efm.volume = EFM_Slider[0].value;
            effectVolume = EFM_Slider[0].value;

            if(efm.volume == 0)
                EFM_Toggle.isOn = false;
            else
                EFM_Toggle.isOn = true;

            SoundPlayer.EFM_value = effectVolume;
        }
        
        PlayerPrefs.SetFloat("BGM",backVolume);
        PlayerPrefs.SetFloat("EFM",effectVolume);
    }

    // 볼륨 온오프 함수
    public void VolumeOnOff(int type)
    {
        if(type == AudioType_BGM)
        {
            if(BGM_Toggle.isOn)
            {
                BGM_Slider[0].value = 1;
                VolumeChange(AudioType_BGM);
            }
            else
            {
                BGM_Slider[0].value = 0;
                VolumeChange(AudioType_BGM);
            }
        }
        if(type == AudioType_EFM)
        {
            if(EFM_Toggle.isOn)
            {
                EFM_Slider[0].value = 1;
                VolumeChange(AudioType_EFM);
            }
            else
            {
                EFM_Slider[0].value = 0;
                VolumeChange(AudioType_EFM);
            }            
        }
        
        PlayerPrefs.SetFloat("BGM",backVolume);
        PlayerPrefs.SetFloat("EFM",effectVolume);
    }
}
