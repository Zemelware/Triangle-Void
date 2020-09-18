using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public float enemySpawnRate = 5f;
    float timer;
    float enemyY;
    float enemyX;
    float enemyXRight;
    float enemyXLeft;
    float screenHeight;
    float screenWidth;

    void Start()
    {
        enabled = true;

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
    }
    
    void Update()
    {
        enemyY = Random.Range(screenHeight + 3, -screenHeight - 3);
        enemyXRight = Random.Range(screenWidth, screenWidth + 3);
        enemyXLeft = Random.Range(-screenWidth, -screenWidth - 3);
        List<float> randomX = new List<float> { enemyXRight, enemyXLeft };
        int randomIndex = Random.Range(0, 2);
        enemyX = randomX[randomIndex];

        // Spawn an enemy every "enemySpawnRate" seconds
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Instantiate(enemy, new Vector3(enemyX, enemyY, 0), new Quaternion(0, 0, 0, 0));
            timer = enemySpawnRate;
        }
    }
}
