using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidBody2D;

    [Header("Movement")]
    public float MovementSpeed = 2.0f;
    public float airMoveSpeed = 30f;
    private float _movementHorizontal;
    private bool _isFacingRight = true;


    [Header("Jummping")]
    public float JumpVelocity = 5f;
    public bool isGrounded = true;
    public Transform GroundCheck;
    public LayerMask groundLayerMask;
    private bool canJump;

    [Header("Wall Sliding")]
    public Transform WallCheck;
    public Vector2 wallCheckSize;
    public float wallSlideSpeed = 0;
    public LayerMask wallLayerMask;
    public bool isTouchingWall;
    public bool isWallSliding;

    [Header("Wall Jumping")]
    public float wallJumpForce = 18f;
    public float wallJumpDirection = -1f;
    public Vector2 wallJumpAngle;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        wallJumpAngle.Normalize();
    }

    private void Update()
    {
        WorldCheck();
        Inputs();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        WallSlide();
        WallJump();
    }

    public void WorldCheck()
    {
        isGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, groundLayerMask);
        isTouchingWall = Physics2D.OverlapBox(WallCheck.position, wallCheckSize, 0, wallLayerMask);

    }

    private void Movement()
    {

        if (_movementHorizontal != 0)
            _animator.SetBool("IsRunning", true);
        else
            _animator.SetBool("IsRunning", false);
        if (isGrounded)
        {
            _rigidBody2D.velocity = new Vector2(_movementHorizontal * MovementSpeed, _rigidBody2D.velocity.y);
        }
        else if(!isGrounded && !isWallSliding && _movementHorizontal != 0)
        {
            _rigidBody2D.AddForce(new Vector2(airMoveSpeed * _movementHorizontal, 0));
        }

        if (_movementHorizontal < 0 && _isFacingRight)
            Flip();
        else if (_movementHorizontal > 0 && !_isFacingRight)
        {
            Flip();
        }

    }

    private void Inputs()
    {
        _movementHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            canJump = true;
    }

    public void Jump()
    {
        if (canJump && isGrounded)
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, JumpVelocity);
        canJump = false;
    }

    private void WallSlide()
    {
        if (isTouchingWall && !isGrounded && _rigidBody2D.velocity.y < 0)
            isWallSliding = true;
        else
            isWallSliding = false;

        if (isWallSliding)
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, wallSlideSpeed);
    }

    private void WallJump()
    {
        if((isWallSliding || isTouchingWall) && canJump)
        {
            _rigidBody2D.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canJump = false;
        }

    }

    private void Flip()
    {
        wallJumpDirection *= -1;
        Vector3 localScale = transform.localScale;

        if (_movementHorizontal > 0.0f)
            _isFacingRight = true;
        else if (_movementHorizontal < 0.0f)
            _isFacingRight = false;

        if (((_isFacingRight) && (localScale.x < 0.0f))
             ||
             ((!_isFacingRight) && (localScale.x > 0.0f))
            )
            localScale.x *= -1.0f;

        transform.localScale = localScale;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, GroundCheck.position);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(WallCheck.position, wallCheckSize);
    }

}
