using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnOff : MonoBehaviour
{
    public void UI_SetActive(GameObject UI_Object)
    {
        UI_Object.SetActive(true);
    }
    public void UI_SetDisable(GameObject UI_Object)
    {
        UI_Object.SetActive(false);
    }
}
