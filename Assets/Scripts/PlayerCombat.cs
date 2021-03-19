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
    //public ParticleSystem deathPartycleEffect;

    public int attackDamage = 50;


    public static bool isDead = false;

    private Vector2 _startingPosition;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _startingPosition = transform.position;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && _playerController.isGrounded && !_playerController.isCrouched && !PauseMenu.gameIsPaused)
        {
            _animator.SetTrigger("Attack");
        }

    }

   public void Attack()
   {
        AudioManager.instance.Play("SwordSwing");
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, canBeDamagedLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().TakeDamage(attackDamage);

            }

    }



    public void Die()
    {
        AudioManager.instance.Play("PlayerDeath");
        //deathPartycleEffect.Play();
        isDead = true;
        _animator.SetBool("IsDead", true);
        _playerController.enabled = false;

        GameManager.Instance.UpdateLives(-1);
        if (GameManager.Instance.Lives > 0)
            Invoke("Respawn", 2);
        else
            GameManager.Instance.DeathScreen.enabled = true;
    }

    public void Respawn()
    {
        isDead = false;
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
