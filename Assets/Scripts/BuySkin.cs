using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
    private int _playerCoins;
    
    private UpdateCoins _updateCoins;

    public Skin skin;
    private TextMeshProUGUI _text;
    private Image _sprite;

    public bool _isBought;

    public static Sprite playerSprite;

    void Start()
    {
        var isBoughtInt = PlayerPrefs.GetInt("isBought" + skin.id);
        
        if (isBoughtInt == 0) _isBought = false;
        else if (isBoughtInt == 1) _isBought = true;

        _updateCoins = FindObjectOfType<UpdateCoins>();

        _playerCoins = PlayerPrefs.GetInt("Player Coins", 0);

        if (!_isBought)
        {
            _text = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>()[0];
            // _text = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            _text.text = $"{skin.cost} Coins";
            
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);  
        }

        _sprite = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _sprite.sprite = skin.sprite;

        if (skin.id == 1) // This is for the default white triangle
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            _isBought = true;
            PlayerPrefs.SetInt("isBought" + skin.id, 1); // 1 is true, 0 is false

            if (PlayerPrefs.GetInt("SelectedSkinID") == -1 || PlayerPrefs.GetInt("SelectedSkinID") == 0)
            {
                playerSprite = skin.sprite;
                PlayerPrefs.SetInt("SelectedSkinID", 1);
            }

        }

        if (_isBought)
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }

    }

    public void OnEnable()
    {
        if (PlayerPrefs.GetInt("SelectedSkinID", 1) == skin.id)
        {
            SkinsManager.SelectASkin(gameObject);
            playerSprite = skin.sprite;
        }
    }

    public void ShowWarningPopup()
    {
        AudioManager.instance.Play("Button Click");

        if (!_isBought)
        {
            _playerCoins = PlayerPrefs.GetInt("Player Coins", 0);
            
            if (_playerCoins < skin.cost) // Make sure the player has enough coins to buy the skin
            {
                // Display message to tell player they don't have enough coins
                var dimScreen = GameObject.Find("Dim Screen");
                var notEnoughCoinsPopup = GameObject.Find("Not Enough Coins Popup");
                
                dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 100);
                dimScreen.GetComponent<Image>().raycastTarget = true;
                notEnoughCoinsPopup.GetComponent<CanvasGroup>().alpha = 1;
                notEnoughCoinsPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                var dimScreen = GameObject.Find("Dim Screen");
                var warningPopup = GameObject.Find("Warning Popup");

                dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 100);
                dimScreen.GetComponent<Image>().raycastTarget = true;
                
                warningPopup.GetComponentInChildren<Text>().text =
                    $"Are you sure you want to buy this skin for {skin.cost} coins?";
                
                warningPopup.GetComponent<CanvasGroup>().alpha = 1;
                warningPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;

                warningPopup.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PurchaseSkin);
            }
        }
        else
        {
            SelectSkin();
        }
    }

    public void PurchaseSkin()
    {
        AudioManager.instance.Play("Button Click");

        var dimScreen = GameObject.Find("Dim Screen");
        var warningPopup = GameObject.Find("Warning Popup");
        
        dimScreen.GetComponent<Image>().color = new Color32(20, 20, 20, 0);
        dimScreen.GetComponent<Image>().raycastTarget = false;
        warningPopup.GetComponent<CanvasGroup>().alpha = 0;
        warningPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;

        // Take away the coins from the player then update the coins text
        _playerCoins -= skin.cost;
        PlayerPrefs.SetInt("Player Coins", _playerCoins);

        if (_playerCoins < 0)
        {
            _playerCoins = 0;
            PlayerPrefs.SetInt("Player Coins", 0);
        }

        _updateCoins.UpdateCoinsText();

        // Remove the lock and cost to show that the user purchased the skin already
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

        // Set the players skin to the one they bought
        playerSprite = skin.sprite;

        // Change the selected skin id to the skin that was purchased
        PlayerPrefs.SetInt("SelectedSkinID", skin.id);
        
        // Show this skin as the selected skin
        SkinsManager.SelectASkin(gameObject);

        _isBought = true;
        PlayerPrefs.SetInt("isBought" + skin.id, 1); // 1 is true, 0 is false
        
        PlayerPrefs.SetInt("lastBoughtID", skin.id);

        if (skin.id != PlayerPrefs.GetInt("lastBoughtID", 1))
        {
            GetComponentInChildren<Image>().color = Color.white;
        }
        
        if (PlayerPrefs.GetInt("lastBoughtID", 0) == skin.id)
        {
            foreach (var obj in FindObjectsOfType<BuySkin>())
            {
                obj.GetComponentInChildren<Image>().color = Color.white;
            }
            
            GetComponentInChildren<Image>().color = Color.cyan;
        }
    }

    public void SelectSkin()
    {
        AudioManager.instance.Play("Button Click");
        
        // Player already bought the skin so update the player's skin to be the one they select
        playerSprite = skin.sprite;
    
        // Change the selected skin id
        PlayerPrefs.SetInt("SelectedSkinID", skin.id);

        // Show this skin as the selected skin
        SkinsManager.SelectASkin(gameObject);
    }

}
