using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour
{
    Rigidbody2D _rigidBody2D;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        if (_rigidBody2D.velocity.y < 0)
        {
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }
}
