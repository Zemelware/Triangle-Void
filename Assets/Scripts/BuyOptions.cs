using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyOptions : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        var gridLayout = GetComponent<GridLayoutGroup>();

        if (Screen.width == 2732 && Screen.height == 2048  || Screen.width == 2732 && Screen.height == 2048 || Screen.width == 1024 && Screen.height == 768 || Screen.width == 2048 && Screen.height == 1536 || Screen.width == 2224 && Screen.height == 1668 || Screen.width == 960 && Screen.height == 640 || Camera.main.aspect == 1.33333333f)
        {
            gridLayout.cellSize = new Vector2(185, 270);
            gridLayout.spacing = new Vector2(10f, 0);
        }
        else
        {
            gridLayout.cellSize = new Vector2(210, 270);
            gridLayout.spacing = new Vector2(60, 0);
        }
#endif
    }
}