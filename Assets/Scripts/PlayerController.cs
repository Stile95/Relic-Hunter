using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    [Header("Movement")]
    public float movementHorizontal;
    public float movementSpeed = 3.0f;
    public bool isWalking;
    public bool isGrounded;
    public bool isFacingRight = true;
    public bool isTouchingWall;
    public bool isCrouched;
    public Vector2 groundCheckSize;


    [Header("Jumping")]
    public float jumpForce = 16.0f;
    public bool canJump;
    public float movementForceInAir;
    public bool isJumping;

    [Header("Wall jumping and sliding")]
    public float wallCheckDistance;
    public float wallSlidingSpeed;
    public float wallJumpForce;
    public Vector2 wallJumpDirection;
    public bool isWallSliding;


    [Header("Other")]
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public Transform groundCheck;


    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        wallJumpDirection.Normalize();
    }



    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckIfJumping();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckWorld();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && _rigidBody2D.velocity.y < 0)
            isWallSliding = true;
        else
            isWallSliding = false;
    }

    private void CheckIfJumping()
    {
        if (_rigidBody2D.velocity.y != 0 && !isGrounded)
            isJumping = true;
        else
            isJumping = false;

    }

    private void CheckWorld()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer);
    }

    private void CheckIfCanJump()
    {

        if ((isGrounded && _rigidBody2D.velocity.y != 0 ) || isWallSliding)
            canJump = true;
        else
            canJump = false;
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementHorizontal < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementHorizontal > 0)
        {
            Flip();
        }

        if (_rigidBody2D.velocity.x != 0 && isGrounded && movementHorizontal != 0) 
            isWalking = true;
        else
            isWalking = false;
    }

    private void CheckInput()
    {
        if(!isCrouched)
        movementHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.S))            //crouch mehanika
        {
            isCrouched = true;
            _boxCollider2D.enabled = false;
        }
        else if (isGrounded && Input.GetKeyUp(KeyCode.S))
        {
            isCrouched = false;
            _boxCollider2D.enabled = true;
        }

        if(isCrouched)
        {
            movementHorizontal = 0;
        }
    }

    private void Jump()
    {
        if(canJump && !isCrouched)
        _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpForce);

        else if((isWallSliding || isTouchingWall) && movementHorizontal != 0 && canJump)
        {
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementHorizontal, wallJumpForce * wallJumpDirection.y);
            _rigidBody2D.AddForce(forceToAdd, ForceMode2D.Impulse);

        }
    }


    private void ApplyMovement()
    {
        if (isGrounded)
        {
            _rigidBody2D.velocity = new Vector2(movementHorizontal * movementSpeed, _rigidBody2D.velocity.y);

        }
        else if (!isGrounded && !isWallSliding && movementHorizontal != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementHorizontal, 0);
            _rigidBody2D.AddForce(forceToAdd);

            if (Mathf.Abs(_rigidBody2D.velocity.x) > movementSpeed)   
                _rigidBody2D.velocity = new Vector2(movementSpeed * movementHorizontal, _rigidBody2D.velocity.y);    
        }
        
        if (isWallSliding)
        {
            if(_rigidBody2D.velocity.y < -wallSlidingSpeed)
            {
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("IsWalking", isWalking);
        _animator.SetBool("IsCrouched", isCrouched);
        _animator.SetBool("IsJumping", isJumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
            transform.parent = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
            transform.parent = null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position,groundCheckSize);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

    }
}
