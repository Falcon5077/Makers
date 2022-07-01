using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRain : MonoBehaviour
{
    public List<GameObject> spawnPosition;
    public GameObject bulletPrefab;
    protected Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = new GameObject();
            temp.transform.parent = this.transform;
            temp.transform.localPosition = new Vector3(-3f + (i * 0.75f), 1.5f, 0);
            temp.name = "RainPosition" + i.ToString();
            spawnPosition.Add(temp);
        }

        InvokeRepeating("Raining", 0f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }


    void Raining()
    {
        for(int i = 0; i < spawnPosition.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition[i].transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            bullet.GetComponent<Bullet>().bulletSpeed = 5;
            bullet.GetComponent<Bullet>().bulletLifeTime = 7;
            
        }
    }
}
