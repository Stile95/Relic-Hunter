using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip PickupAudioClip;
    public ParticleSystem CoinParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")

            return;

        //AudioSource.PlayClipAtPoint(PickupAudioClip, transform.position);

        GameManager.Instance.UpdateCoins(1);

        //CoinParticleSystem.transform.SetParent(null);
        //CoinParticleSystem.Play();

        Destroy(gameObject);
    }
        
}
