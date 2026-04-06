using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem dash;
    [SerializeField] private ParticleSystem movementparticles;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float dashCooldown = 1f;

    private bool facingLeft = true;
    private Vector2 currentVelocity = Vector2.zero;

    private ParticleSystem.EmissionModule movementEmission;
    private float nextDashTime = 0f;

    void Start()
    {
        movementEmission = movementparticles.emission;
        movementparticles.Play();
        movementEmission.enabled = false;
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (DrunkManager.Instance == null)
        {
            Debug.LogError("DrunkManager.Instance is null!");
            return;
        }

        Vector2 targetVelocity = Vector2.zero;
        bool isMoving = false;

        if (Input.GetKey(KeyCode.D))
        {
            targetVelocity = Vector2.right * speed;
            player.transform.localScale = new Vector3(-1, 1, 1);
            facingLeft = false;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetVelocity = Vector2.left * speed;
            player.transform.localScale = new Vector3(1, 1, 1);
            facingLeft = true;
            isMoving = true;
        }

        //velocity with acceleration and deceleration
        currentVelocity = Vector2.Lerp(playerRigidbody.linearVelocity, targetVelocity, (targetVelocity != Vector2.zero ? acceleration : deceleration) * Time.deltaTime);
        playerRigidbody.linearVelocity = currentVelocity;

        // Dash with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime)
        {
            if (DrunkManager.Instance != null && DrunkManager.Instance.GetDrunkValue() >= 0.25f)
            {
                if (dash != null)
                    dash.Play();

                if (playerRigidbody != null)
                {
                    Vector2 dashDirection = facingLeft ? Vector2.left : Vector2.right;
                    playerRigidbody.AddForce(dashDirection * speed * 5f, ForceMode2D.Impulse);
                }

                DrunkManager.Instance.ReduceDrunkValue(0.25f);
                nextDashTime = Time.time + dashCooldown;
            }
        }

        // Toggle movement particles
        movementEmission.enabled = isMoving;
    }
}
