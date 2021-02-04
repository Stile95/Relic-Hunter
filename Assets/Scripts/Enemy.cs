using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator _animator;

    public int maxHeatlh = 100;
    int currentHealth;


    void Awake()
    {
        currentHealth = maxHeatlh;
        _animator = GetComponent<Animator>();

    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        _animator.SetTrigger("Damaged");

        if (currentHealth <= 0)
            Die();
    }


    public void Die()
    {
        _animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true; 
        this.enabled = false;
    }

    
}
