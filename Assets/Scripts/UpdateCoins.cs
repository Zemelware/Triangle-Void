using TMPro;
using UnityEngine;

public class UpdateCoins : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int playerCoins;
    
    void Start()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        playerCoins = PlayerPrefs.GetInt("Player Coins", 0);
        text.text = $"Coins: {playerCoins}";
    }

}
