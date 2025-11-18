using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public float jumpCooldown = 0.3f;
    private float nextJumpTime = 0f;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump with cooldown
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= nextJumpTime)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                nextJumpTime = Time.time + jumpCooldown;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Check if player is above lava
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // contact.normal points *from the lava to the player*
                if (contact.normal.y > 0.5f)
                {
                    // Only take damage if touching lava from above
                    TakeDamage(20);
                    break;
                }
            }
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // Stop play mode in the Editor
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Level Completed!");
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Level Completed!");

        // Stop play mode in the Editor
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
