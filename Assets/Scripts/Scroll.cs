using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;
 
    float leftPosX = 0f;
    float rightPosX = 0f;

    void Start()
    {
        leftPosX = -6.5f;
        rightPosX = 6.5f;
    }

    void Update()
    {
       for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;
 
            if(backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(rightPosX , nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}