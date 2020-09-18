using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1500f;
    
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    
    public Joystick joystick;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        // Stop the player from going past the screen
        Vector3 playerPos = transform.position;

        float maxY = Camera.main.orthographicSize;
        playerPos.y = Mathf.Clamp(transform.position.y, -maxY + 0.3f, maxY - 0.3f); 
        
        float maxX = maxY * Camera.main.aspect;
        playerPos.x = Mathf.Clamp(transform.position.x, -maxX + 0.3f, maxX - 0.3f);
        
        transform.position = playerPos;

        // This movement code only applies if your playing the game on the computer, or in the unity editor
#if UNITY_STANDALONE
        joystick.gameObject.SetActive(false);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotate player toward mouse
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        // Controls
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector2.up * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector2.down * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
        }
#endif
        
        // This movement code only applies if your playing the game on a phone, or in the unity editor
#if UNITY_IOS || UNITY_ANDROID
        // Creating a horizontal and vertical move variable. joystick.Horizontal and joystick.Vertical are float
        // values of 0 - 1 based on how much the joystick is moved in each direction
        float horizontalMove = joystick.Horizontal * movementSpeed * Time.deltaTime;
        float verticalMove = joystick.Vertical * movementSpeed * Time.deltaTime;

        // Add force to the RigidBody based on the horizontal and vertical move
        rb.AddForce(new Vector2(horizontalMove, verticalMove));

        rb.angularVelocity = 0f;
        if (horizontalMove == 0f && verticalMove == 0f)
        {
            rb.velocity = Vector2.zero;
        }
#endif
        
    }

}
