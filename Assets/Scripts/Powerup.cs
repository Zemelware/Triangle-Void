using TMPro;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float tweenSpeed = 1f;
    public float powerupDuration = 10f;
    private int oldBulletDamage;

    public HitEnemy hitEnemy;
    
    private Score scoreScript;
    private GameObject damageIncreasedText;
    private TextMeshPro t;

    private void Start()
    {
        oldBulletDamage = hitEnemy.bulletDamage;

        // Delete the powerup if the player hasn't got it after a certain amount of time
        Invoke("DeletePowerup", 5f);

        LeanTween.moveY(gameObject, transform.position.y + 0.8f, tweenSpeed).setEaseInOutSine().setLoopPingPong();
    }

    private void DeletePowerup()
    {
        Destroy(gameObject);
    }
    
    private void ShowDamageIncreasedText()
    {
        damageIncreasedText = new GameObject("Damage +5");
        damageIncreasedText.transform.position = new Vector3(x: transform.position.x, transform.position.y + 0.3f, 0f);
        t = damageIncreasedText.AddComponent<TextMeshPro>();
        t.alignment = TextAlignmentOptions.Center;
        t.fontSize = 3;
        t.text = "Damage +5";
        t.color = new Color(255, 255, 255);
    }

    private void DeleteDamageIncreasedText()
    {
        Destroy(damageIncreasedText);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CancelInvoke("DeletePowerup");
        
        // If it's not the player who collided with the powerup, do nothing
        if (collision.gameObject.name != "Player") return;
        
        AudioManager.instance.Play("Powerup");
        
        // Turn off the powerups collider
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        
        // Tell the player that their damage increased by 5
        ShowDamageIncreasedText();
        Invoke("DeleteDamageIncreasedText", 0.5f);

        hitEnemy.bulletDamage = 10;

        // Make the powerup invisible
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // Make powerup only last a certain amount of time
        Invoke("StopPowerup", powerupDuration);
    }

    private void StopPowerup()
    {
        hitEnemy.bulletDamage = oldBulletDamage;
        Destroy(gameObject);
    }
}
