using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public int coinsAmount;
    
    public void OnPurchaseComplete(Product product)
    {
        var coins = PlayerPrefs.GetInt("Player Coins");
        coins += coinsAmount;
        PlayerPrefs.SetInt("Player Coins", coins);

        var text = GameObject.Find("Coins Text").GetComponent<TextMeshProUGUI>();
        text.text = $"Coins: {coins}";
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning("Purchase of: " + product.definition.id + " failed due to " + reason);
    }
    
}
