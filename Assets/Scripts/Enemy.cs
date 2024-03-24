using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float headDetectionRange = .15f;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float patrolRange = 2f;
    [SerializeField]
    private int health = 1;

    
    private GameObject player;
    private PlatformerPlayer playerScript;
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private float leftPatrolLimit;
    private float rightPatrolLimit;
    private bool movingRight = true;


    void Start()
    {
        
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

        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        leftPatrolLimit = initialPosition.x - patrolRange / 2f;
        rightPatrolLimit = initialPosition.x + patrolRange / 2f;
    }

    void Update()
    {
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
    }
    void Flip()
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            ContactPoint2D contact = collision.GetContact(0);
            float capsuleCenterY = transform.position.y + headDetectionRange;


            if (contact.point.y > capsuleCenterY)
            {
            health--;
            if(health <= 0){
            playerScript.EnemyKilled();
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
}
