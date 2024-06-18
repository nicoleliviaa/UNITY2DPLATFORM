using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;  // Ensure the initial velocity is set
        Destroy(gameObject, 1f);  // Destroy the projectile after 1 second
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        rb.velocity = direction * speed;

        // Flip the sprite if necessary
        if (direction == Vector2.left)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
        else if (direction == Vector2.right)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);  // Destroy the projectile on collision with any object except the player
        }
    }
}

