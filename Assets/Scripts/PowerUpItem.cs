using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the Shooting component from the player
            Shooting shootingComponent = collision.GetComponent<Shooting>();

            if (shootingComponent != null)
            {
                // Activate the power-up effect in the Shooting component
                shootingComponent.EatPowerUp();
            }

            // Destroy the power-up item after collision with the player
            Destroy(gameObject);
        }
    }
}
