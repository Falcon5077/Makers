using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource mAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        // mAudioSource 초기화
        mAudioSource = this.GetComponent<AudioSource>();

        // 다른 씬에서 사용될 수 있게 DontDestroy
        DontDestroyOnLoad(this.gameObject);
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
