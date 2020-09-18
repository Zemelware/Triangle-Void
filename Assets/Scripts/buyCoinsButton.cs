using UnityEngine;

public class buyCoinsButton : MonoBehaviour
{
    void Start()
    {
#if !UNITY_IOS && !UNITY_ANDROID
        gameObject.SetActive(false);
#endif
    }
}
