using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private Animator _animator;
    private PlayerController _playerController;


    public Transform attackPoint;
    public float attackRange;
    public LayerMask canBeDamagedLayer;

    public int attackDamage = 50;

    public Vector2 _startingPosition;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && _playerController.isGrounded && !_playerController.isCrouched)
        {
            _animator.SetTrigger("Attack");
        }

       
    }

   public void Attack()
   {
       
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, canBeDamagedLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().TakeDamage(attackDamage);

            }
    }



    public void Die()
    {
        _animator.SetBool("IsDead", true);
        _playerController.enabled = false;
        // TODO death particle effect

        GameManager.Instance.UpdateLives(-1);

        Invoke("Respawn", 2);
    }

    public void Respawn()
    {
        transform.position = _startingPosition;
        _animator.SetBool("IsDead", false);
        _playerController.enabled = true;

    }


    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }






}
