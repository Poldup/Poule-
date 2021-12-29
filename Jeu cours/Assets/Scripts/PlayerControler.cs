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
    public bool infiniteJump;



    public KeyCode toucheSaut;
    public KeyCode toucheGlide;
    public KeyCode toucheGlide2;


    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Transform wallCheckUpLeft;
    public Transform wallCheckDownLeft;
    public Transform wallCheckUpRight;
    public Transform wallCheckDownRight;

    public Material defmat;
    public Material whiteMat;

    [HideInInspector]
    public float axeHorizontal;
    private float jumpTimer;
    private float currentGlideTimer;
    private float canJumpAgain = 0.2f;
    private float knockTimer;

    [HideInInspector]
    public int comptPlumes;
    public bool isInvincible;
    
    public bool canMove;
    public bool playing;
    private bool jumpKeyGot;
    private bool jumpKeyGotDown;
    private bool glideKeyGot;
    private bool isJumping;
    private bool isGliding;
    public bool isGrounded;
    private bool onWallRight;
    private bool onWallLeft;
    private bool onWallRightRel;
    private bool onWallLeftRel;
    private bool notKnocked;
    private Vector2 velocity = Vector2.zero;
    

    public static PlayerControler Instance;

    private void Awake()
    {
        playing = true;
        Instance = this;
        canMove = true;
    }

    void Update()
    {
        GetKeys();
        IsJumping();
        IsGliding();
        AnimTriggers();
    }

    void FixedUpdate()
    {
        SurroundingDetector();
        Flip(rb.velocity.x);

        MovePlayer();
        Jump();
        Glide();

        //if (knockTimer>0)
        //{ knockTimer = Mathf.Clamp(knockTimer-Time.deltaTime,0,100); }
        
    }

    void GetKeys()
    {
        if (playing)
        {
            jumpKeyGot = Input.GetKey(toucheSaut);
            jumpKeyGotDown = Input.GetKeyDown(toucheSaut);
            glideKeyGot = Input.GetKey(toucheGlide) | Input.GetKey(toucheGlide2);
            axeHorizontal = Input.GetAxis("Horizontal");
        }
    }

    

    void AnimTriggers()
    {
        //envoie les triggers ? l'animator
        float absVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", absVelocity);
        animator.SetBool("Grounded", isGrounded);
    }


    void SurroundingDetector()
    {
        //D?tecte le sol et les murs graces aux gameobjects (les points verts autour de la poule)
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, platformLayerMask);
        onWallLeftRel = Physics2D.OverlapArea(wallCheckDownLeft.position, wallCheckUpLeft.position, platformLayerMask);
        onWallRightRel = Physics2D.OverlapArea(wallCheckDownRight.position, wallCheckUpRight.position, platformLayerMask);
    }

    void Flip(float _velocity)
    {
        bool sensGauche=true;
        //si la poule se d?place vers la droite ou la gauche, son gameobject se tourne dans cette direction, les d?tecteurs de murs relatifs sont permut?s si besoin car ils s'inversent lors du flip
        if(axeHorizontal>0 && canMove)
        {
            poule.transform.localScale = new Vector3(-1, 1, 1);
            sensGauche = false;
        }
        
        else if(axeHorizontal<0 && canMove)
        {
            poule.transform.localScale = new Vector3(1, 1, 1);
            sensGauche = true;
        }

        if(sensGauche)
        {onWallLeft = onWallLeftRel; onWallRight = onWallRightRel;}
        else
        {onWallRight = onWallLeftRel; onWallLeft = onWallRightRel;}
    }

    void MovePlayer()
    {
        // horizontalmouvement correspond ? l'input de l'axe horizontal (correspond ? z/d ou </>) x movespeed x le temps qui passe
        float _horizontalMovement = axeHorizontal * moveSpeed * Time.fixedDeltaTime;
        
        //S'il y a un mur ? gauche, le mouvement vertical est limit? vers la droite, et r?ciproquement
        if (onWallLeft)
        {
            _horizontalMovement = Mathf.Clamp(_horizontalMovement, 0, 1000);
        }
        else if(onWallRight)
        {
            _horizontalMovement = Mathf.Clamp(_horizontalMovement, -1000, 0);
        }
        
        //application du mouvement horizontal au rigibody avec un smoothdamp, une acc?l?ration l?g?rement progressive
        Vector2 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        if (knockTimer==0 && canMove)
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
            notKnocked = true;
        }
    }

    void IsJumping()
    {
        //Si le joueur appuie sur espace, que le compteur de plumes dispo n'est pas ? 0 et que le timer pour sauter ? nouveau est ? z?ro, la poule va sauter
        if (jumpKeyGotDown)
        {
            if ( infiniteJump || (((comptPlumes > 0 && canJumpAgain == 0) | isGrounded) && currentGlideTimer > 0 && knockTimer == 0 && canMove) )
            {
                //le timer pour sauter ? nouveau passer ? 0.2, le compteur de plumes dispo diminue de 1
                canJumpAgain = 0.2f;
                notKnocked = true;
                if (!isGrounded)
                { GameManager.Instance.AddPlume(-1); }

                //la poule passe en ?tat de saut, le timer de saut commence ? z?ro, trigger de saut pour l'animator
                isJumping = true;
                jumpTimer = 0;
                animator.SetTrigger("Jump");

                //rb.velocity = Vector2.zero; (NE SERT A RIEN ?)
            }
        }
    }


    void Jump()
    {
        
        //Quand la poule est au sol, le compteur de plumes disponibles revient  ? z?ro et le compteur pour sauter ? nouveau aussi
        if (isGrounded)
        {
            GameManager.Instance.ResetPlumes();
            comptPlumes = Mathf.Clamp(GameManager.Instance.goldenPlumes + GameManager.Instance.plumes, 0, 100);
            canJumpAgain = 0;

        }
        
        //?tat de saut
        if (isJumping)
        {
            //la vitesse verticale de la poule est multipli? par jumpForce, le timer de saut augmente avec le temps
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer += Time.deltaTime;
            
            //Si le joueur ne maintient pas espace au del? du temps de saut, la vitesse verticale retombe ? maxjumpspeed, la poule va bient?t redescendre
            if (jumpTimer >= jumpDuration && !jumpKeyGot)
            {
                isJumping = false; 
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, 0, maxJumpSpeed));
            }
            //? la fin du temps de saut long (m?me si le joueur maintient espace), la vitesse verticale retombe ? maxjumpspeed, la poule va bient?t redescendre
            else if (jumpTimer >= longJumpDuration)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y * 1.1f, 0, maxJumpSpeed));
            }
        }
        
        //Si le timer pour sauter ? nouveau est sup?rieur ? 0, il diminue avec le temps jusqu'? z?ro
        if(canJumpAgain>0)
        {
            canJumpAgain = Mathf.Clamp(canJumpAgain - Time.deltaTime, 0, 0.2f);

        }
    }

    void IsGliding()
    {
        if (canMove && rb.velocity.y < 0 && !isGrounded && currentGlideTimer > 0 && !jumpKeyGotDown && glideKeyGot && knockTimer==0 && jumpTimer>jumpDuration && !onWallLeft && !onWallRight)
        {
            isGliding = true;
            notKnocked = true;
        }
        else
        {
            isGliding = false;
        }
    }

    void Glide()
    {
        
        if(isGrounded)
        {
            currentGlideTimer = glideTimer;
        }
        
        if (isGliding)
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

    public void Damage(Vector2 contact, bool knocking)
    {
        if (!isInvincible)
        {
            StartCoroutine(Hit(contact, knocking));
        }
    }

    public IEnumerator Hit(Vector2 contact,bool knocking)
    {
        if (isInvincible)
        { yield break; }
        else
        {
            IEnumerator blink;
            blink = Blink();
            canMove = false;
            GameManager.Instance.TakeDamage();
            StartCoroutine(blink);
            isInvincible = true;
            Debug.Log(knocking);
            if (knocking)
            {
                Vector2 knockDir;

                if (contact.y > transform.position.y + 0.4375 || (contact.x > transform.position.x + 0.4975 && contact.x < transform.position.x + 0.605 && contact.y > transform.position.y + 0.249))
                {
                    //haut
                    knockDir = new Vector2(0, -10);
                    //Debug.Log("haut");
                }
                else if (contact.y > transform.position.y - 0.936 && contact.x > transform.position.x)
                {
                    //droite
                    knockDir = new Vector2(-5, 2);
                    //Debug.Log("droite");
                }
                else if (contact.y > transform.position.y - 0.936 && contact.x < transform.position.x)
                {
                    //gauche
                    knockDir = new Vector2(5, 2);
                    //Debug.Log("gauche");
                }
                else if (contact.y < transform.position.y - 0.936 && rb.velocity.x > 0.1)
                {
                    //bas en allant vers droite
                    knockDir = new Vector2(-3.75f, 5);
                    //Debug.Log("bas en allant vers la droite");
                }
                else if (contact.y < transform.position.y - 0.936 && rb.velocity.x < -0.1)
                {
                    //bas en allant vers gauche
                    knockDir = new Vector2(3.75f, 5);
                    //Debug.Log("bas en allant vers la gauche");
                }
                else
                {
                    //bas vertical
                    knockDir = new Vector2(0, 5);
                    //Debug.Log("bas vertical");
                }
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(knockDir, ForceMode2D.Impulse);
                yield return new WaitForSeconds(.5f);
            }
            Debug.Log("weshh");
            
            canMove = true;
            Debug.Log("wesh");
            yield return new WaitForSeconds(1);
            isInvincible = false;
            
            StopCoroutine(blink);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = defmat;

        }

       
    }

    public IEnumerator Blink()
    {
        while(true)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = whiteMat;
            yield return new WaitForSeconds(.1f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = defmat;
            yield return new WaitForSeconds(.1f);
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

