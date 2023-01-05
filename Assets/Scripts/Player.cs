using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    bool facingRight = true;

    bool backAtWall;
    bool isGrounded;
    bool isOnTopOfWall;
    public Transform groundCheck;
    public float groundCheckRadius;
    public float wallCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public float jumpForce;

   
    bool isOnWall;
    public Transform wallCheck;

    public float xWallForce;
    public float yWallForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //right & left movement
        float playerInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(playerInput * speed, rb.velocity.y);

        //jumpwall
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        if (isOnWall == true && isGrounded == false)
        {
            if (Input.GetKey(KeyCode.S) == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
            }

            if (Input.GetKey(KeyCode.S) && (Input.GetKeyDown(KeyCode.A) == false | Input.GetKeyDown(KeyCode.D) == false))
            {
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            } else if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                if (Input.GetKey(KeyCode.W))
                {
                    rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(xWallForce * playerInput, yWallForce);
                }
            }
        }
        if (isGrounded == true)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        
        
        
        //flip sprite to face direction
        if (playerInput > 0 && facingRight == false)
        {
            Flip();
        } else if (playerInput < 0 && facingRight == true) {
            Flip();
        }

        //jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnTopOfWall = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsWall);
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded == true | (isOnTopOfWall == true)))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    
    
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

}
