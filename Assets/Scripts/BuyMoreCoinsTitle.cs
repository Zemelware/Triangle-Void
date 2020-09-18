using TMPro;
using UnityEngine;

public class BuyMoreCoinsTitle : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (Screen.width == 2732 && Screen.height == 2048  || Screen.width == 2732 && Screen.height == 2048 ||  Screen.width == 1024 && Screen.height == 768 || Screen.width == 2048 && Screen.height == 1536 || Screen.width == 2224 && Screen.height == 1668 || Screen.width == 960 && Screen.height == 640 || Camera.main.aspect == 1.33333333f)
        {
            GetComponent<TextMeshProUGUI>().fontSize = 65;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().fontSize = 75;
        }
#endif
    }
}
