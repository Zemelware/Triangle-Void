using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Transform player;
    public Rigidbody2D rb;
    public float speed = 150f;
    public float accuracy = 0.1f;
    Vector3 direction;

    void Start()
    {
        enabled = true;
        
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        direction = player.position - transform.position;
        if (direction.magnitude > accuracy)
        {
            rb.AddForce(direction.normalized * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            rb.AddForce(-direction.normalized * 300 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

}
