using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    void Start()
    {
#if !UNITY_IOS && !UNITY_ANDROID
        gameObject.SetActive(false);
#endif        
    }
}
