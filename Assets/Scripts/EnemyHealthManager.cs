using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int MaxHealth = 100;
    int currentHealth;

    public ParticleSystem DeathParticlePrefab;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            AudioManager.instance.Play("EnemyDeath");
            Die();
        }

    }

    private void Die()
    {
        DeathParticlePrefab.Play();
        Destroy(gameObject);
    }



}
