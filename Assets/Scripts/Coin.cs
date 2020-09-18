using UnityEngine;

public class Coin : MonoBehaviour
{
    public UpdateCoins updateCoins;
    public SpriteRenderer spriteRenderer;
    private Score score;

    private int playerCoins;
    private int coinIncrease = 1;
    
    void Start()
    {
        score = GameObject.Find("Game Manager").GetComponent<Score>();
        
        playerCoins = PlayerPrefs.GetInt("Player Coins");

        Invoke("DeleteCoin", 8f);
    }

    void Update()
    {
        if (score.score >= 30 && score.score < 60)
        {
            coinIncrease = 3;
            spriteRenderer.color = new Color32(36, 102, 229, 255);
        }
        else if (score.score >= 60)
        {
            coinIncrease = 5;
            spriteRenderer.color = new Color32(231, 23, 45, 255);
        }
        else
        {
            coinIncrease = 1;
            spriteRenderer.color = new Color32(233, 219, 17, 255);
        }
    }

    private void DeleteCoin()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CancelInvoke("DeleteCoin");

        if (other.gameObject.name == "Player")
        {
            AudioManager.instance.Play("Coin");
            playerCoins += coinIncrease;
            PlayerPrefs.SetInt("Player Coins", playerCoins);
            updateCoins.UpdateCoinsText();

            Destroy(gameObject);
        }

        if (other.gameObject.name == "Enemy(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
