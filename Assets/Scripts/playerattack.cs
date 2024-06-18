using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public GameObject shootingItem;  // Prefab to shoot
    public Transform firePoint;  // Where the projectile spawns
    public float projectileFiringPeriod = 0.1f;  // Time between shots

    private bool hasPowerUp = false;  // Flag to track if player has power-up
    private Coroutine shootingCoroutine;

    private void Update()
    {
        if (hasPowerUp && Input.GetButtonDown("Fire1"))  // Check if player has power-up and presses fire button
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
            }
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            GameObject ball = Instantiate(shootingItem, firePoint.position, Quaternion.identity);
            ShootingItem shootingItemComponent = ball.GetComponent<ShootingItem>();

            // Set direction based on player's facing direction
            if (transform.localScale.x > 0f)
            {
                shootingItemComponent.SetDirection(Vector2.right);
            }
            else
            {
                shootingItemComponent.SetDirection(Vector2.left);
            }

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    // Method to "eat" the power-up
    public void EatPowerUp()
    {
        hasPowerUp = true;
        // Optionally, you can disable the power-up item GameObject here
    }
}
