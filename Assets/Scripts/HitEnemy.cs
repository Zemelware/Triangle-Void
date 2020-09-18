using UnityEngine;
using TMPro;

public class HitEnemy : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyMinHealth;
    public int enemyCurrentHealth;
    public int bulletDamage = 5;
    public GameObject impactEffect;
    [HideInInspector] public float enemyScale;
    private int enemyHealth;

    private Score scoreScript;
    private GameObject enemyHealthText;
    private TextMeshPro t;

    // Particle system explosion
    public GameObject explosion;
    ParticleSystem flare;
    ParticleSystem explosionParticles;
    public ParticleSystem.EmissionModule flareEmission;
    public ParticleSystem.EmissionModule explosionEmission;
    public ParticleSystem.ShapeModule explosionShape;
    public ParticleSystem.VelocityOverLifetimeModule explosionVOL;

    private void Start()
    {
        // Particle System Explosion--------------------------
        flare = explosion.GetComponent<ParticleSystem>();
        explosionParticles = explosion.transform.GetChild(0).GetComponent<ParticleSystem>();
        flareEmission = flare.emission;
        explosionEmission = explosionParticles.emission;
        explosionShape = explosionParticles.shape;
        explosionVOL = explosionParticles.velocityOverLifetime;
        //-----------------------------------------------------
        
        scoreScript = GameObject.Find("Game Manager").GetComponent<Score>();

        // Generate random enemy health value in a range
        enemyHealth = Random.Range(enemyMinHealth, enemyMaxHealth + 1);
        enemyCurrentHealth = enemyHealth;

        // Make the scale of the enemy smaller or larger based on it's health
        enemyScale = (enemyHealth * 0.04f) + 0.5f;
        enemyScale = Mathf.Clamp(enemyScale, 0, 6);
        transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);

        // Put text in enemy that shows it's health
        enemyHealthText = new GameObject("Enemy Health");
        enemyHealthText.transform.localScale = transform.localScale;
        t = enemyHealthText.AddComponent<TextMeshPro>();
        t.alignment = TextAlignmentOptions.Center;
        t.fontSize = 5;
        t.text = enemyHealth.ToString();
        t.color = new Color(0, 0, 0);
    }

    private void LateUpdate()
    {
        // Move text so it stays on the enemy
        t.transform.localPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            AudioManager.instance.Play("Player Hit"); // The player hit sound should also be played when the enemy gets hit
            
            // Show the impact sparks when we hit the enemy
            var impactGO = Instantiate(impactEffect, GameObject.FindGameObjectWithTag("Bullet").transform.position, Quaternion.identity);
            Destroy(impactGO, 2f);
            
            Destroy(GameObject.FindGameObjectWithTag("Bullet"));

            TakeDamage(bulletDamage);
            t.text = enemyCurrentHealth.ToString();
            
            if (enemyCurrentHealth <= 0) // When enemy dies
            {
                AudioManager.instance.Play("Enemy Explode");

                Destroy(enemyHealthText);
                scoreScript.addScore();
                
                // Make the explosion bigger if the enemy is bigger, then instantiate the explosion
                flareEmission.rateOverTime = 150 / (6 / transform.localScale.x);
                explosionEmission.rateOverTime = 7500 / (6 / transform.localScale.x);
                explosionShape.radius = 2 / (6 / transform.localScale.x);
                explosionVOL.radial = 15 / (6 / transform.localScale.x);

                var explosionGO = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explosionGO, 2f);
                
                Destroy(gameObject);
            }
        }
    }

    private void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
    }

}
