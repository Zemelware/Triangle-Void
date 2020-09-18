using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    Transform gun;
    Vector3 direction;
    float screenHeight;
    float screenWidth;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        gun = GameObject.Find("Gun").GetComponent<Transform>();
        direction = gun.up;

        transform.position = gun.position;
    }

    void LateUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // If a bullet goes off the screen, then despawn it
        if (transform.position.x > screenWidth + 0.1f || transform.position.x < -screenWidth - 0.1f || transform.position.y > screenHeight + 0.1f || transform.position.y < -screenHeight - 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
