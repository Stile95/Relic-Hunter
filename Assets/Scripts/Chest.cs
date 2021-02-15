using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int currentHealth = 1;
    public int value = 3;

    public ParticleSystem ChestParticleSystem;

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
            DestroyChest();
    }

    public void DestroyChest()
    {
        GameManager.Instance.UpdateCoins(value);
        //ChestParticleSystem.transform.SetParent(null);
        //ChestParticleSystem.Play();
        Destroy(gameObject);

    }


}
