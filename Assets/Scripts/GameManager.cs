using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Gun gun;
    public MoveEnemy enemyMovement;
    public SpawnEnemies spawnEnemies;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public HitEnemy hitEnemy;
    public GameObject menuScreen;
    public GameObject skinsScreen;
    public GameObject optionsScreen;
    public GameObject buyCoinsScreen;
    
    [HideInInspector]
    public bool gameOver;
    bool paused;

    public void FreeCoins() // Just for testing
    {
        var coins = PlayerPrefs.GetInt("Player Coins");
        coins += 1000;
        PlayerPrefs.SetInt("Player Coins", coins);
        
        Debug.Log("I am programming right now.");
        
        var text = GameObject.Find("Coins Text").GetComponent<TextMeshProUGUI>();
        text.text = $"Coins: {coins}";

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        gameOver = false;
        paused = false;
        hitEnemy.bulletDamage = 5;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Player") != null && !gameOver)
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }

        if (!enemyMovement.enabled && !gameOver && !paused && GameObject.Find("Enemy(Clone)") != null)
        {
            GameObject.Find("Enemy(Clone)").GetComponent<MoveEnemy>().enabled = true;
            enemyMovement.enabled = true;
        }

        if (gameOver)
        {
            gameOverScreen.SetActive(true);

            playerMovement.enabled = false;
            enemyMovement.enabled = false;
            spawnEnemies.enabled = false;
            gun.enabled = false;
            
            // Stop the player from moving
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.rb.angularVelocity = 0;

            // Remove any enemies from the scene
            Destroy(GameObject.Find("Enemy(Clone)"));
            Destroy(GameObject.Find("Enemy Health"));

            Invoke("SetTimeScaleZero", 0.8f);

            gameOver = false;
        }
    }

    private void SetTimeScaleZero()
    {
        Time.timeScale = 0f;
    }
    
    #if UNITY_IOS || UNITY_ANDROID
    public void GetMoreCoins()
    {
        AdController.instance.ShowRewardedVideoAd();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.Play("Button Click");
    }
    #endif

    public void Restart()
    {
        Time.timeScale = 1f;

        AudioManager.instance.Play("Button Click");
        
        playerMovement.enabled = true;
        enemyMovement.enabled = true;
        spawnEnemies.enabled = true;
        gun.enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        Time.timeScale = 1f;

#if UNITY_IOS || UNITY_ANDROID
        AdController.instance.ShowBannerAd();
#endif

        AudioManager.instance.Play("Button Click");

        enemyMovement.enabled = true;
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
#if UNITY_IOS || UNITY_ANDROID
        AdController.instance.HideBannerAd();
#endif

        AudioManager.instance.Unpause("Music"); // If the player goes to the menu from the pause screen, then we need to unpause the music
        AudioManager.instance.Play("Button Click");

        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        AudioManager.instance.Pause("Music");
        
        paused = true;

        pauseScreen.SetActive(true);

        Time.timeScale = 0f;

        playerMovement.enabled = false;
    }

    public void Resume()
    {
        AudioManager.instance.Unpause("Music");
        AudioManager.instance.Play("Button Click");

        paused = false;

        pauseScreen.SetActive(false);

        Time.timeScale = 1f;
        
        playerMovement.enabled = true;
    }

    public void ShowSkinsScreen()
    {
        AudioManager.instance.Play("Button Click");

        skinsScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void BackFromSkinsScreen()
    {
        AudioManager.instance.Play("Button Click");

        skinsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void ShowOptionsScreen()
    {
        AudioManager.instance.Play("Button Click");

        optionsScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void BackFromOptionsScreen()
    {
        AudioManager.instance.Play("Button Click");

        optionsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void ResetGameData()
    {
        AudioManager.instance.Play("Button Click");
    
        var coinsText = GameObject.Find("Coins Text").GetComponent<TextMeshProUGUI>();
        coinsText.text = "Coins: 0";

        menuScreen.SetActive(true);
        var bestText = GameObject.Find("Best").GetComponent<TextMeshProUGUI>();
        bestText.text = "BEST: 0";
        menuScreen.SetActive(false);

        skinsScreen.SetActive(true);
        GameObject itemsParent = GameObject.Find("Items Parent");
        SkinsManager.skins = itemsParent.GetComponentsInChildren<BuySkin>();
        skinsScreen.SetActive(false);

        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HideNotEnoughCoinsPopup()
    {
        AudioManager.instance.Play("Button Click");
    
        var dimScreen = GameObject.Find("Dim Screen");
        var notEnoughCoinsPopup = GameObject.Find("Not Enough Coins Popup");
        
        dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 0);
        dimScreen.GetComponent<Image>().raycastTarget = false;
        notEnoughCoinsPopup.GetComponent<CanvasGroup>().alpha = 0;
        notEnoughCoinsPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void ShowWarningResetPopup()
    {
        AudioManager.instance.Play("Button Click");
        
        var dimScreen = GameObject.Find("Dim Screen");
        var warningPopup = GameObject.Find("Warning Popup Reset");
        
        dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 100);
        dimScreen.GetComponent<Image>().raycastTarget = true;
        warningPopup.GetComponent<CanvasGroup>().alpha = 1;
        warningPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void HideWarningPopup()
    {
        AudioManager.instance.Play("Button Click");

        var dimScreen = GameObject.Find("Dim Screen");
        var warningPopupReset = GameObject.Find("Warning Popup Reset");
        var warningPopup = GameObject.Find("Warning Popup");
        
        dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 0);
        dimScreen.GetComponent<Image>().raycastTarget = false;
        warningPopupReset.GetComponent<CanvasGroup>().alpha = 0;
        warningPopupReset.GetComponent<CanvasGroup>().blocksRaycasts = false;
        warningPopup.GetComponent<CanvasGroup>().alpha = 0;
        warningPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        
        warningPopup.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void ShowBuyCoinsScreen()
    {
        AudioManager.instance.Play("Button Click");

        buyCoinsScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void HideBuyCoinsScreen()
    {
        AudioManager.instance.Play("Button Click");

        buyCoinsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

}