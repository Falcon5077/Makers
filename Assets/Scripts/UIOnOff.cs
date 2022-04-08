using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UI_SetActive(GameObject UI_Object)
    {
        UI_Object.SetActive(true);
    }
    public void UI_SetDisable(GameObject UI_Object)
    {
        UI_Object.SetActive(false);
    }
}
