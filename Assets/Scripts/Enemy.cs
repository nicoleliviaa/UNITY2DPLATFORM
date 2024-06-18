using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManagerScript gameManagerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManagerScript.gameOver();
        }
        else if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);  // Destroy the projectile
            Destroy(gameObject);  // Destroy the enemy
        }
    }
}

