using UnityEngine;

public class Parachute : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalSpeed;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Pick random direction at spawn
        direction = Random.value < 0.5f ? -1 : 1;

        // Random horizontal speed
        horizontalSpeed = Random.Range(0.5f, 1f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * horizontalSpeed, rb.linearVelocity.y);
    }


    // Collision checks for different objects
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Parachute hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            DestroyParachute(false);
            ScoreManager.Instance.AddPoint(); // add a point
        }
        else if (other.CompareTag("Floor"))
        {
            DestroyParachute(true); // spawn splash
            ScoreManager.Instance.GetDamage(); // take a point of damage
        }
        else if (other.CompareTag("Wall"))
        {
            direction *= -1; // reverse direction of parachute when it hits a wall
        }
    }

    void DestroyParachute(bool hitFloor) // check if it hit the floor to spawn splash
    {
        ParachuteManager manager = Object.FindFirstObjectByType<ParachuteManager>();
        if (manager != null)
        {
            manager.RemoveParachute(gameObject, hitFloor);
        }
    }
}
