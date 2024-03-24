using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float headDetectionRange = .15f;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float patrolRange = 5f;
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private float inversionInterval = 1f;
    [SerializeField]
    private float jumpIntervalMin = 5f;
    [SerializeField]
    private float jumpIntervalMax = 15f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private GameObject particleGameObject;

    private float timeSinceLastChange;
    private float nextJumpTime;

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private float leftPatrolLimit;
    private float rightPatrolLimit;
    private bool movingRight = true;

    private GameObject player;
    private PlatformerPlayer playerScript;
    private SpriteRenderer enemyRenderer;
    private BoxCollider2D enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();

        enemyCollider = GetComponent<BoxCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        if(player!=null){
            playerScript = player.GetComponent<PlatformerPlayer>();
        }

        if(player == null){
            Debug.Log("Player game object not found");
        }
        if(playerScript == null){
            Debug.Log("Platformer Player Script not found");
        }
        if(enemyRenderer == null){
            Debug.Log("Platform Renderer not found");
        }

        timeSinceLastChange = 0f;
        CalculateNextJumpTime();

        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        leftPatrolLimit = initialPosition.x - patrolRange / 2f;
        rightPatrolLimit = initialPosition.x + patrolRange / 2f;

        HideEnemy();
    }
        
    

    // Update is called once per frame
    void Update()
    {
        // Accumulate time since last change
        timeSinceLastChange += Time.deltaTime;

        // Check if it's time to invert the boolean variable
        if (timeSinceLastChange >= inversionInterval)
        {
            // Invert the boolean variable randomly
            movingRight = Random.Range(0, 2) == 0 ? true : false;

            // Reset the time since last change
            timeSinceLastChange = 0f;
        }
        
        if (Time.time >= nextJumpTime)
        {
            Jump();
            CalculateNextJumpTime();
        }


        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (transform.position.x >= rightPatrolLimit)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (transform.position.x <= leftPatrolLimit)
            {
                Flip();
            }
        }
        if(playerScript != null && playerScript.EnemyCounter() >=3){
            ShowEnemy();
        }
        
    }


    void ShowEnemy(){
        enemyRenderer.enabled = true;
        enemyCollider.enabled = true;
    }
    void HideEnemy(){
        enemyRenderer.enabled = false;
    }
    

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void CalculateNextJumpTime()
    {
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }


    void Flip()
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyRenderer.enabled == true)
        {

            ContactPoint2D contact = collision.GetContact(0);
            float capsuleCenterY = transform.position.y + headDetectionRange;


            if (contact.point.y > capsuleCenterY)
            {
            health--;
            PlayParticleEffect(transform.position);
            if(health <= 0){
            playerScript.EnemyKilled();
            playerScript.bossKilled();
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = .01f;
            rb.velocity = new Vector2(0f, -10f); 
            gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
            }

            }
            else if(contact.point.y < capsuleCenterY){
                Destroy(collision.gameObject);
            }
        }
    }
        void PlayParticleEffect(Vector3 position)
        {
        // Instantiate the particle system prefab at the specified position
        GameObject particleSystemInstance = Instantiate(particleGameObject, position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
        // Play the particle system
        particleSystem.Play();

        // Destroy the particle system after its duration
        Destroy(particleSystemInstance, particleSystem.main.duration);
        }
}

