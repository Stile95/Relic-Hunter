using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int MaxHealth = 100;
    int currentHealth;

    public GameObject DeathParticlePrefab;



    private void Awake()
    {
        currentHealth = MaxHealth;

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
      //  Instantiate(DeathParticlePrefab);

        Destroy(gameObject);
    }



}
