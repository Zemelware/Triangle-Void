using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int enemyDamage = 20;
    
    public GameManager gameManager;
    public HealthBar healthBar;

    [SerializeField] private Sprite _defaultSprite;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = BuySkin.playerSprite;

        if (PlayerPrefs.GetInt("SelectedSkinID") == 1)
            GetComponent<SpriteRenderer>().sprite = _defaultSprite;

        GetComponent<SpriteRenderer>().sprite = SkinsManager.skins[PlayerPrefs.GetInt("SelectedSkinID", 1) - 1].skin.sprite;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if (currentHealth <= 0)
        {
            gameManager.gameOver = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Enemy(Clone)")
        {
            TakeDamage(enemyDamage);
            AudioManager.instance.Play("Player Hit");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
