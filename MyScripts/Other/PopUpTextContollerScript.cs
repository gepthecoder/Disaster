using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextContollerScript : MonoBehaviour
{
    private static PopUpTextScript popUpText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        popUpText = Resources.Load<PopUpTextScript>("MyPrefabs/GameComponents/PopUpTxtParent1");
    }


    public static void CreateFloatingText(string text, Transform location)
    {
        PopUpTextScript instance = Instantiate(popUpText);
        Vector2 screenPosition = new Vector2(Screen.width/2 + Random.Range(-.5f, .5f), Screen.height/2 + Random.Range(-.5f, .5f));

        Debug.Log("PopUpText----->" + screenPosition);

        instance.transform.SetParent(canvas.transform);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
