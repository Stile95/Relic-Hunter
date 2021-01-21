using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    private float movementHorizontal;

    public float movementSpeed = 3.0f;
    public float jumpForce = 16.0f;
    public float wallCheckDistance;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.05f;
    public Vector2 groundCheckSize;

    private bool isFacingRight = true;
    private bool isWalking;
    [SerializeField]private bool isGrounded;
    private bool isTouchingWall;
    private bool canJump;
    private bool isWallSliding;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }



    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
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

    private void CheckWorld()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer);

    }

    private void CheckIfCanJump()
    {

        if (isGrounded && _rigidBody2D.velocity.y <= 0)
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

        if (_rigidBody2D.velocity.x != 0)
            isWalking = true;
        else
            isWalking = false;
    }

    private void CheckInput()
    {
        movementHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if(canJump)
        _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpForce);

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

            if (Mathf.Abs(_rigidBody2D.velocity.x) > movementSpeed)    //too study
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
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position,groundCheckSize);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

    }
}
