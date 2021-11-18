using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed;
    public float maxJumpSpeed;
    public float jumpForce;
    public float jumpDuration;


    private float jumpTimer;
    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    private void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector2 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            jumpTimer = jumpDuration;
            rb.velocity = Vector2.zero;
            print(rb.velocity);
        }
        if(isJumping)
        {
            rb.velocity = transform.up * jumpForce;
            jumpTimer -= Time.deltaTime;
            print(rb.velocity);
            if (jumpTimer<=0)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, 0, maxJumpSpeed));
            }
        }
    }
}


/*public float speed = 10.5f;
public float jumpStrength = 50.2f;
public Rigidbody2D rb;

public KeyCode rightKey;
public KeyCode leftKey;
public KeyCode jumpKey;

public bool grounded;
public bool onWallRigth;
public bool onWallLeft;

public float distance = 1.5f;
public float heigthDecal = -0.15f;

public float playerSize = 0.1f;

public float fallMultiplier = 1.1f;
public float maxFallSpeed = -10;

public Animator animator;
public SpriteRenderer spriterenderer;



void Update()
{
    WallRightDectection();
    WallLeftDectection();
    LateralMove();
    DetectGround();
    JumpMove();
    FallAcceleration();
    // ManageAnimations();

}

void WallRightDectection()
{
    Vector3 origin = transform.position + new Vector3(playerSize, heigthDecal, 0);
    Vector3 direction = new Vector3(1, 0, 0);
    Color rayColor;

    if (Physics2D.Raycast(origin, direction, distance))
    {
        onWallRigth = true;
        rayColor = Color.green;
    }
    else
    {
        onWallRigth = false;
        rayColor = Color.red;
    }

    Debug.DrawRay(origin, direction * distance, rayColor);
}

void WallLeftDectection()
{
    Vector3 origin = transform.position + new Vector3(-playerSize, heigthDecal, 0);
    Vector3 direction = new Vector3(-1, 0, 0);
    Color rayColor;

    if (Physics2D.Raycast(origin, direction, distance))
    {
        onWallLeft = true;
        rayColor = Color.green;
    }
    else
    {
        onWallLeft = false;
        rayColor = Color.red;
    }

    Debug.DrawRay(origin, direction * distance, rayColor);
}

void LateralMove()
{
    if (Input.GetKey(rightKey))
    {
        if (onWallRigth == false)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    if (Input.GetKey(leftKey))
    {
        if (onWallLeft == false)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}

void JumpMove()
{
    if (grounded == true)
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
        }
    }
}

void DetectGround()
{
    Vector3 origin = transform.position + new Vector3(0, heigthDecal, 0);
    Vector3 direction = new Vector3(0, -1, 0);
    Color rayColor;

    if (Physics2D.Raycast(origin, direction, distance))
    {
        grounded = true;
        rayColor = Color.green;
    }
    else
    {
        grounded = false;
        rayColor = Color.red;
    }

    Debug.DrawRay(origin, direction * distance, rayColor);
}

void FallAcceleration()
{
    if (rb.velocity.y < 0)
    {

        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, maxFallSpeed, 0));
    }
}

/*
void ManageAnimations()
{
    if (rb.velocity.magnitude > 0.01f)
    {
        animator.Play("frogrun");
    }
    else
    {
        animator.Play("chickenidle");
    }
    if (Input.GetKey(leftKey))
    {
        spriterenderer.flipX = true;
    }

    if (Input.GetKey(rightKey))
    {
        spriterenderer.flipX = false;
    }
}


public void GameOver()
{
    Debug.Log("Game Over");

    string sceneName = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

}*/
