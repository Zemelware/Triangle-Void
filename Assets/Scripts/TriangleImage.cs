using System;
using UnityEngine;

public class TriangleImage : MonoBehaviour
{
    private void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        var cam = Camera.main;
        
        if (Screen.width == 960 && Screen.height == 640 || Screen.width == 2732 && Screen.height == 2048 || Camera.main.aspect == 1.33333333f)
        {
            var position = GetComponent<RectTransform>().position;
            position.x = -5f;
            position.y = -8.75f;
            GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f);
        }
        else
        {
            var position = GetComponent<RectTransform>().position;
            position.x = 29f;
            position.y = -53f;
            GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
        }
#endif  
    }
 
}
