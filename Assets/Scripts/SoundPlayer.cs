using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource mAudioSourve;
    // Start is called before the first frame update
    void Start()
    {
        mAudioSourve = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mAudioSourve.Play();
        }
    }
}
