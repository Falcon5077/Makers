using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource efm;
    public Slider[] BGM_Slider;
    public Slider[] EFM_Slider;
    public float backVolume;
    public float effectVolume;

    public Toggle BGM_Toggle;
    public Toggle EFM_Toggle;
    private const int AudioType_BGM = 1;
    private const int AudioType_EFM = 2;
    // Start is called before the first frame update
    void Start()
    {
        backVolume = PlayerPrefs.GetFloat("BGM",1);
        effectVolume = PlayerPrefs.GetFloat("EFM",1);
        bgm.volume = backVolume;
        efm.volume = effectVolume;
        BGM_Slider[0].value = backVolume;
        EFM_Slider[0].value = effectVolume;
        Debug.Log(backVolume + "," + effectVolume);
    }

    public void VolumeChange(int type)
    {
        if(type == AudioType_BGM)
        {
            bgm.volume = BGM_Slider[0].value;
            backVolume = BGM_Slider[0].value;
        }
        if(type == AudioType_EFM)
        {
            efm.volume = EFM_Slider[0].value;
            effectVolume = EFM_Slider[0].value;
        }
        
        PlayerPrefs.SetFloat("BGM",backVolume);
        PlayerPrefs.SetFloat("EFM",effectVolume);
        Debug.Log(backVolume + "," + effectVolume);
    }

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
        Debug.Log(backVolume + "," + effectVolume);

    }
}
