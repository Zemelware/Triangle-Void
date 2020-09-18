using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public float bulletShootDelay = 0.1f;
    
    float timer;

#if UNITY_IOS || UNITY_ANDROID
    private Vector3 touchPos;
    TouchDetector detector;
    Transform player;
#endif

    void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        detector = FindObjectOfType<TouchDetector>();
        player = GameObject.Find("Player").transform;
#endif
    
        enabled = true;
    }

    void Update()
    {
#if UNITY_STANDALONE
        // Spawn bullet, but only allow a bullet to spawned every "bulletShootDelay" seconds
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                AudioManager.instance.Play("Shoot");
                Instantiate(bullet);
                timer = bulletShootDelay;
            }
        }
#endif
        
        
#if UNITY_IOS || UNITY_ANDROID
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            if (detector.isTouching)
            {
                foreach (var t in Input.touches)
                {
                    if (t.fingerId == detector.currentTouchID)
                    {
                        AudioManager.instance.Play("Shoot");
                        
                        touchPos = Camera.main.ScreenToWorldPoint(t.position);
                        
                        player.rotation = Quaternion.LookRotation(Vector3.forward, touchPos - player.position);
                        
                        Instantiate(bullet);
                        timer = bulletShootDelay;
                    }
                }
            }
        }

#endif

    }

}
