using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public GameObject player;
    public ParticleSystem dash;
    public ParticleSystem movementparticles;

    public float speed = 10f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float dashCooldown = 1f; // 1 second cooldown for the dash

    private bool facingLeft = true;
    private Vector2 currentVelocity = Vector2.zero;

    private ParticleSystem.EmissionModule movementEmission;
    private float nextDashTime = 0f; // when the player can dash again

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
            dash.Play();
            Vector2 dashDirection = facingLeft ? Vector2.left : Vector2.right;
            playerRigidbody.AddForce(dashDirection * speed * 5f, ForceMode2D.Impulse);

            nextDashTime = Time.time + dashCooldown; // set next allowed dash
        }

        // Toggle movement particles
        movementEmission.enabled = isMoving;
    }
}
