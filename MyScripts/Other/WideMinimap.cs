using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideMinimap : MonoBehaviour
{
    public FixedButton wideMinimapBtn;
    public GameObject wideMap;

    public static bool WideMapIsOpen = false;

    void Update()
    {
        if (wideMap != null)
        {
            if (wideMinimapBtn.Pressed)
            {            
                OpenWideMap();  
            }
        }
    }

    private void OpenWideMap()
    {
        wideMap.SetActive(true);

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        WideMapIsOpen = true;
    }

    public void CloseWideMap()
    {
        wideMap.SetActive(false);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        WideMapIsOpen = false;
    }
}
