using CloudOnce;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    void Start()
    {
        #if UNITY_IOS || UNITY_ANDROID
        Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
        Cloud.Initialize(false, true);
        Leaderboards.TopScores.SubmitScore(PlayerPrefs.GetInt("highScore", 0));
        #endif
    }

    public void CloudOnceInitializeComplete()
    {
        #if UNITY_IOS || UNITY_ANDROID
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
        #endif
    }
}
