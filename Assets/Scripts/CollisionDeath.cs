using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !PlayerCombat.isDead)
        {
            PlayerCombat.FindObjectOfType<PlayerCombat>().Die();

        }

        else
            return;

    }


}
