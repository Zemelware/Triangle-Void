using UnityEngine;

public class SpawnHeart : MonoBehaviour
{
    [HideInInspector] public float heartSpawnRateMax = 90f;
    [HideInInspector] public float heartSpawnRateMin = 45f;
    private float _heartSpawnRate;

    private float _screenWidth;
    private float _screenHeight;
    private float _heartX;
    private float _heartY;

    [SerializeField] private GameObject heart;
    
    private float _timer;
    private bool _startGame;
    
    void Start()
    {
        _screenHeight = Camera.main.orthographicSize;
        _screenWidth = _screenHeight * Camera.main.aspect;
        
        _startGame = true;
    }

    void Update()
    {
        // Set the random position
        _heartX = Random.Range(-_screenWidth + 0.1f, _screenWidth - 0.1f);
        _heartY = Random.Range(-_screenHeight + 0.1f, _screenHeight - 0.1f);

        // Set the random spawn rate
        _heartSpawnRate = Random.Range(heartSpawnRateMin, heartSpawnRateMax);
        
        // Actually spawn the heart
        spawnHeart();
    }

    private void spawnHeart()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            if (_startGame)
                _startGame = false;
            else
                Instantiate(heart, new Vector3(_heartX, _heartY), Quaternion.identity);

            _timer = _heartSpawnRate;
        }
    }
}
