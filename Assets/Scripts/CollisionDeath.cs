using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{

    public PlayerCombat _playerCombat;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("HIT");
            _playerCombat.GetComponent<PlayerCombat>().Die();

        }

        else
            return;

    }


}
