using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int healthToAdd = 20;
    
    private HealthBar healthBar;
    private PlayerHit playerHit;
    
    void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        playerHit = GameObject.Find("Player").GetComponent<PlayerHit>();
        
        // Delete the heart after a bit of time
        Invoke("deleteHeart", 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure only the player can collide with the heart
        if (other.gameObject.name != "Player") return;

        AudioManager.instance.Play("Heart");
        
        CancelInvoke("deleteHeart");

        IncreaseHealth(healthToAdd);
        
        // Delete the heart from the hierarchy
        Destroy(gameObject);
    }

    private void deleteHeart()
    {
        Destroy(gameObject);
    }
    
    private void IncreaseHealth(int amountToAdd)
    {
        playerHit.currentHealth += amountToAdd;
        
        healthBar.SetHealth(playerHit.currentHealth);
    }

}
