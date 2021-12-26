using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject spriteGameObject;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = spriteGameObject.GetComponent<SpriteRenderer>();
        animator = spriteGameObject.GetComponent<Animator>();
    }

    void Update()
    {
        spriteRenderer.flipX = playerMovement.GetFlipX();
        animator.SetFloat("speed", Mathf.Abs(playerMovement.GetHorizontalInput()));

        animator.SetBool("grounded", playerMovement.IsGrounded());
    }
}
