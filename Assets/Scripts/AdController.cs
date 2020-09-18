using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour, IUnityAdsListener 
{
    public static AdController instance;
    
#if UNITY_IOS
    private string gameId = "3549185";
#elif UNITY_ANDROID
    private string gameId = "3549184";
#endif

    private string _videoAd = "video";
    private string _rewardedVideoAd = "rewardedVideo";
    private string _bannerAd = "BannerAd";


    private void Awake()
    {
#if !UNITY_IOS && !UNITY_ANDROID
        Destroy(gameObject);
#endif

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

#if UNITY_IOS || UNITY_ANDROID
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false); // Test mode needs to be turned to false when releasing the game on the app store
    }

    public void ShowVideoAd()
    {
        if (Advertisement.IsReady(_videoAd))
        {
            Advertisement.Show(_videoAd);
        }
    }

    public void ShowRewardedVideoAd()
    {
        if (Advertisement.IsReady(_rewardedVideoAd))
        {
            Advertisement.Show(_rewardedVideoAd);
        }
    }

    public void ShowBannerAd()
    {
        if (Advertisement.IsReady(_bannerAd))
        {
            Advertisement.Banner.Show(_bannerAd);
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
#endif

    public void OnUnityAdsReady(string placementId)
    {
        // Ad is ready to be played
    }

    public void OnUnityAdsDidError(string message)
    {
        // Some error occured
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Ad started
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            if (placementId == _rewardedVideoAd)
            {
                // Reward the player with coins
                var coins = PlayerPrefs.GetInt("Player Coins");
                coins += 15;
                PlayerPrefs.SetInt("Player Coins", coins);

                var text = GameObject.Find("Coins Text").GetComponent<TextMeshProUGUI>();
                text.text = $"Coins: {coins}";
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Don't reward the player
        }
        else if (showResult == ShowResult.Failed)
        {
            // Ad didn't finish because of an error
        }

    }

}
