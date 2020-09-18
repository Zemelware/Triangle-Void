using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public float powerupSpawnRateMax = 60f;
    public float powerupSpawnRateMin = 30f;
    float powerupSpawnRate;
    
    float screenWidth;
    float screenHeight;
    float powerupX;
    float powerupY;

    public GameObject powerup;
    public HitEnemy hitEnemy;
    
    private float timer;
    private bool startGame = true;
    
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        hitEnemy.bulletDamage = 5;
        startGame = true;
    }

    void Update()
    {
        // Set powerupSpawnRate to random value between the min and max
        powerupSpawnRate = Random.Range(powerupSpawnRateMin, powerupSpawnRateMax);
        
        // Spawn a powerup every so often
        SpawnPU();
    }

    private void SpawnPU()
    { 
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (startGame)
                startGame = false;
            else
            {
                // Set the powerup x and y to be random values so the powerup spawns in a random position
                powerupX = Random.Range(-screenWidth + 0.5f, screenWidth - 0.5f);
                powerupY = Random.Range(-screenHeight + 0.5f, screenHeight - 0.5f);

                Instantiate(powerup, new Vector3(powerupX, powerupY, 0f), new Quaternion(0f, 0f, 0f, 0f));
            }

            timer = powerupSpawnRate;
        }

    }

}
