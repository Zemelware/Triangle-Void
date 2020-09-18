using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public class Score : MonoBehaviour
{
    public HitEnemy hitEnemyScript;
    public SpawnEnemies spawnEnemiesScript;
    public Gun gunScript;
    public SpawnPowerup spawnPowerup;
    public SpawnHeart spawnHeart;
    
    public int score = 0;
    public int highScore;

    [HideInInspector]
    public int counter = 0;

    Text scoreText;

    void Start()
    {
        PlayerPrefs.GetInt("highScore");

        hitEnemyScript.enemyMaxHealth = 10;
        hitEnemyScript.enemyMinHealth = 5;
        gunScript.bulletShootDelay = 0.15f;
        spawnEnemiesScript.enemySpawnRate = 5f;
        spawnHeart.heartSpawnRateMax = 90f;
        spawnHeart.heartSpawnRateMin = 45f;
        score = 0;
        counter = 0;
        
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void addScore()
    {
        score++;
        counter++;

        #if UNITY_IOS || UNITY_ANDROID
        Leaderboards.TopScores.SubmitScore(score);
        #endif

        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }

        // What to do every time the score increases by 5
        if (counter == 5)
        {
            spawnHeart.heartSpawnRateMax -= 5f;
            spawnHeart.heartSpawnRateMin -= 2f;
            spawnHeart.heartSpawnRateMax = Mathf.Clamp(spawnHeart.heartSpawnRateMax, 10f, spawnHeart.heartSpawnRateMax);
            spawnHeart.heartSpawnRateMin = Mathf.Clamp(spawnHeart.heartSpawnRateMin, 3f, spawnHeart.heartSpawnRateMin);
            
            spawnPowerup.powerupSpawnRateMax -= 10f;
            spawnPowerup.powerupSpawnRateMin -= 4f;
            spawnPowerup.powerupSpawnRateMax = Mathf.Clamp(spawnPowerup.powerupSpawnRateMax, 5f, spawnPowerup.powerupSpawnRateMax);
            spawnPowerup.powerupSpawnRateMin = Mathf.Clamp(spawnPowerup.powerupSpawnRateMin, 0.1f, spawnPowerup.powerupSpawnRateMin);
            

            hitEnemyScript.enemyMaxHealth += 5;
            hitEnemyScript.enemyMinHealth = hitEnemyScript.enemyMaxHealth / 2;
            
            spawnEnemiesScript.enemySpawnRate -= 0.5f;
            spawnEnemiesScript.enemySpawnRate = Mathf.Clamp(spawnEnemiesScript.enemySpawnRate, 1f, spawnEnemiesScript.enemySpawnRate);

            gunScript.bulletShootDelay -= 0.015f;
            gunScript.bulletShootDelay = Mathf.Clamp(gunScript.bulletShootDelay, 0.001f, gunScript.bulletShootDelay);

            counter = 0;
        }
    }
}
