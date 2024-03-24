using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
[SerializeField]
private float speed = 3.0f;
[SerializeField]
private float jumpForce = 5.0f;

private bool bossAlive = true;
private int coins = 0;
private int enemiesKilled = 0;
private Rigidbody2D body;
private BoxCollider2D box;
private Animator animator;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        if(bossAlive){
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        // Flip the sprite if moving left
            if (deltaX < 0)
                transform.localScale = new Vector3(-.5f, .5f, .5f);
            else if (deltaX > 0) // Reset scale if moving right
                transform.localScale = new Vector3(.5f, .5f, .5f);


        animator.SetFloat("xVelocity", Mathf.Abs(deltaX));
        animator.SetFloat("yVelocity", body.velocity.y);
        animator.SetBool("isJumping", !IsGrounded());

        body.gravityScale = (IsGrounded() && Mathf.Approximately(deltaX, 0)) ? 0: 1;
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W))) {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        }
        else{

        }

    }
    public void CoinAdded(){
        coins++;
    }
    public int CoinsCollected(){
        return coins;
    }
    public void EnemyKilled(){
        enemiesKilled++;
    }
    public int EnemyCounter(){
        return enemiesKilled;
    }
    public void bossKilled(){
        bossAlive = false;
    }
    public bool bossStatus(){
        return bossAlive;
    }
    private bool IsGrounded(){
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        //check below the collider’s min and max values
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        return hit != null;
    }
}