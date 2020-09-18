using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public float coinSpawnRateMax = 15f;
    public float coinSpawnRateMin = 3f;
    private float coinSpawnRate;
    
    float screenWidth;
    float screenHeight;
    float coinX;
    float coinY;

    public GameObject coin;
    
    private float timer;
    private bool startGame = true;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        startGame = true;
    }

    void Update()
    {
        coinX = Random.Range(-screenWidth + 0.1f, screenWidth - 0.1f);
        coinY = Random.Range(-screenHeight + 0.1f, screenHeight - 0.1f);

        coinSpawnRate = Random.Range(coinSpawnRateMin, coinSpawnRateMax);

        SpawnCoin();
    }

    private void SpawnCoin()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (startGame)
                startGame = false;
            else
                Instantiate(coin, new Vector3(coinX, coinY), new Quaternion(0f, 0f, 0f, 0f));

            timer = coinSpawnRate;
        }
    }
    
}
