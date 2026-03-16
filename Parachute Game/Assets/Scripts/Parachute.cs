using UnityEngine;

public class Parachute : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalSpeed;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.value < 0.5f ? -1 : 1; // Pick random direction at spawn
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
            ScoreManager.Instance.AddPoint();
        }
        else if (other.CompareTag("Floor"))
        {
            DestroyParachute(true);
            ScoreManager.Instance.GetDamage();
        }
        else if (other.CompareTag("Wall"))
        {
            direction *= -1;
        }
    }

    void DestroyParachute(bool hitFloor)
    {
        ParachuteManager manager = Object.FindFirstObjectByType<ParachuteManager>();
        if (manager != null)
        {
            manager.RemoveParachute(gameObject, hitFloor);
        }
    }
}
