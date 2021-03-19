using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip PickupAudioClip;
    public ParticleSystem CoinParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && PlayerCombat.isDead)

            return;

        AudioManager.instance.Play("Coins");

        GameManager.Instance.UpdateCoins(1);

        CoinParticleSystem.transform.SetParent(null);
        CoinParticleSystem.Play();

        Destroy(gameObject);
    }
        
}
