using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidBody2D;

    [Header("Movement")]
    public float MovementSpeed = 2.0f;
    public float _movementHorizontal;
    private bool _isFacingRight = true;


    [Header("Jummping")]
    public float JumpVelocity = 5f;
    public bool isGrounded = true;
    public Transform GroundCheck;
    public LayerMask groundLayerMask;

    [Header("Wall Sliding")]
    public Transform WallCheck;
    public Vector2 wallCheckSize;
    public float wallSlideSpeed = 0;
    public LayerMask wallLayerMask;
    public bool isTouchingWall;
    public bool isWallSliding;

    [Header("Wall Jumping")]
    public float wallJumpForce;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WorldCheck();
        Move();
    }

    private void LateUpdate()
    {
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

    private void FixedUpdate()
    {
        Jump();
        WallSlide();
    }

    public void WorldCheck()
    {
        isGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, groundLayerMask);
        isTouchingWall = Physics2D.OverlapBox(WallCheck.position, wallCheckSize, 0, wallLayerMask);

    }

    private void Move()
    {
        _movementHorizontal = Input.GetAxis("Horizontal");

        _rigidBody2D.velocity = new Vector2(_movementHorizontal * MovementSpeed, _rigidBody2D.velocity.y);
        _animator.SetBool("IsRunning", _movementHorizontal != 0.0f);
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
            _rigidBody2D.velocity = Vector2.up * JumpVelocity;
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, GroundCheck.position);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(WallCheck.position, wallCheckSize);


    }

}
