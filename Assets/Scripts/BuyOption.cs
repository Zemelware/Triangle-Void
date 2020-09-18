using TMPro;
using UnityEngine;

public class BuyOption : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS
        if (Screen.width == 2732 && Screen.height == 2048 || Screen.width == 1024 && Screen.height == 768)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Number")
                    child.GetComponent<TextMeshProUGUI>().fontSize = 55;
                else if (child.name == "Coins")
                    child.GetComponent<TextMeshProUGUI>().fontSize = 45;
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Number")
                    child.GetComponent<TextMeshProUGUI>().fontSize = 60;
                else if (child.name == "Coins")
                    child.GetComponent<TextMeshProUGUI>().fontSize = 50;
            }
        }
#endif
    }
}
