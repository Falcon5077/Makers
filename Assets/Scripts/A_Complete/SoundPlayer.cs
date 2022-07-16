using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource mAudioSource;
    public Material Blur;
    
    public static float EFM_value = 1;
    // Start is called before the first frame update
    void Start()
    {
        Blur.SetInt("_Radius",0);
        Cursor.lockState = CursorLockMode.None;
        // mAudioSource 초기화
        mAudioSource = this.GetComponent<AudioSource>();
        var objs = FindObjectsOfType<SoundPlayer>();
        if(objs.Length == 1)
        // 다른 씬에서 사용될 수 있게 DontDestroy
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // 클릭 효과음 실행
        if(Input.GetMouseButtonDown(0))
        {
            mAudioSource.Play();
        }
    }
}
