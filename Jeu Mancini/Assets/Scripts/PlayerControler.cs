using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public GameObject poule;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;
    [SerializeField] private LayerMask platformLayerMask;

    public float moveSpeed;
    public float maxJumpSpeed;
    public float jumpForce;
    public float jumpDuration;
    public float longJumpDuration;
    public float glideSpeed;
    public float glideTimer;
    public float fallMultiplier;
    public float maxFallSpeed;
    public int plumes;
    public int comptPlumes;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Transform wallCheckUpLeft;
    public Transform wallCheckDownLeft;
    public Transform wallCheckUpRight;
    public Transform wallCheckDownRight;


    private float jumpTimer;
    private float currentGlideTimer;
    private float canJumpAgain = 0.2f;

    private bool isJumping;
    private bool isGrounded;
    private bool onWallRight;
    private bool onWallLeft;
    private bool onWallRightRel;
    private bool onWallLeftRel;
    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        Jump();
        Glide();
    }

    void FixedUpdate()
    {
        SurroundingDetector();

        Flip(rb.velocity.x);

        MovePlayer();

        AnimTriggers();
        
    }

    void AnimTriggers()
    {
        //envoie les triggers à l'animator
        float absVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", absVelocity);
        animator.SetBool("Grounded", isGrounded);
    }


    void SurroundingDetector()
    {
        //Détecte le sol et les murs graces aux gameobjects (les points verts autour de la poule)
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, platformLayerMask);
        onWallLeftRel = Physics2D.OverlapArea(wallCheckDownLeft.position, wallCheckUpLeft.position, platformLayerMask);
        onWallRightRel = Physics2D.OverlapArea(wallCheckDownRight.position, wallCheckUpRight.position, platformLayerMask);
    }

    void Flip(float _velocity)
    {
        bool sensGauche=true;
        //si la poule se déplace vers la droite ou la gauche, son gameobject se tourne dans cette direction, les détecteurs de murs relatifs sont permutés si besoin car ils s'inversent lors du flip
        if(_velocity > 0.1)
        {
            poule.transform.localScale = new Vector3(-1, 1, 1);
            sensGauche = true;
        }
        
        else if(_velocity < -0.1)
        {
            poule.transform.localScale = new Vector3(1, 1, 1);
            sensGauche = false;
        }

        if(sensGauche)
        {onWallLeft = onWallRightRel; onWallRight = onWallLeftRel;}
        else
        {onWallRight = onWallRightRel; onWallLeft = onWallLeftRel;}
    }

    void MovePlayer()
    {
        // horizontalmouvement correspond à l'input de l'axe horizontal (correspond à z/d ou </>) x movespeed x le temps qui passe
        float _horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        
        //S'il y a un mur à gauche, le mouvement vertical est limité vers la droite, et réciproquement
        if (onWallLeft)
        {
            _horizontalMovement = Mathf.Clamp(_horizontalMovement, 0, 1000);
        }
        else if(onWallRight)
        {
            _horizontalMovement = Mathf.Clamp(_horizontalMovement, -1000, 0);
        }
        
        //application du mouvement horizontal au rigibody avec un smoothdamp, une accélération légèrement progressive
        Vector2 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    void Jump()
    {
        
        //Quand la poule est au sol, le compteur de plumes disponibles revient  à zéro et le compteur pour sauter à nouveau aussi
        if (isGrounded)
        {
            comptPlumes = plumes;
            canJumpAgain = 0;

        }
        
        //Si le joueur appuie sur espace, que le compteur de plumes dispo n'est pas à 0 et que le timer pour sauter à nouveau est à zéro, la poule va sauter
        if (Input.GetKeyDown(KeyCode.Space) && comptPlumes > 0 && canJumpAgain==0 && currentGlideTimer>0)
        {
            //le timer pour sauter à nouveau passer à 0.2, le compteur de plumes dispo diminue de 1
            canJumpAgain = 0.2f;
            comptPlumes = comptPlumes - 1;

            //la poule passe en état de saut, le timer de saut commence à zéro, trigger de saut pour l'animator
            isJumping = true;
            jumpTimer = 0;
            animator.SetTrigger("Jump");

            //rb.velocity = Vector2.zero; (NE SERT A RIEN ?)

        }
        
        //état de saut
        if (isJumping)
        {
            //la vitesse verticale de la poule est multiplié par jumpForce, le timer de saut augmente avec le temps
            rb.velocity = transform.up * jumpForce;
            jumpTimer += Time.deltaTime;
            
            //Si le joueur ne maintient pas espace au delà du temps de saut, la vitesse verticale retombe à maxjumpspeed, la poule va bientôt redescendre
            if (jumpTimer >= jumpDuration && !Input.GetKey(KeyCode.Space))
            {
                isJumping = false; 
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, 0, maxJumpSpeed));
            }
            //à la fin du temps de saut long (même si le joueur maintient espace), la vitesse verticale retombe à maxjumpspeed, la poule va bientôt redescendre
            else if (jumpTimer >= longJumpDuration)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, 0, maxJumpSpeed));
            }
        }
        
        //Si le timer pour sauter à nouveau est supérieur à 0, il diminue avec le temps jusqu'à zéro
        if(canJumpAgain>0)
        {
            canJumpAgain = Mathf.Clamp(canJumpAgain - Time.deltaTime, 0, 0.2f);

        }
    }

    void Glide()
    {
        
        if(isGrounded)
        {
            currentGlideTimer = glideTimer;
        }
        
        if (!isGrounded && currentGlideTimer>0 && !Input.GetKey(KeyCode.Space) && (Input.GetKey("z") | Input.GetKey("up")))
        {
            rb.velocity = new Vector2 (rb.velocity.x, - glideSpeed);
            animator.SetTrigger("Gliding");
            currentGlideTimer = Mathf.Clamp(currentGlideTimer - Time.deltaTime, 0, glideTimer);
            if (currentGlideTimer < glideTimer / 3)
            {
                animator.SetTrigger("EndGlide");
            }
        }
        else
        {
            animator.SetTrigger("StopGlide");
        }
        if(!isGrounded && currentGlideTimer==0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, maxFallSpeed, 0));
            animator.SetTrigger("Falling");
        }
    }


}


/* CODE DU COURS
 * 
 * public float speed = 10.5f;
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

