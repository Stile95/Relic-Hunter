using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value = 1;
    public AudioClip PickupAudioClip;
    public ParticleSystem CoinParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")

            return;

        //AudioSource.PlayClipAtPoint(PickupAudioClip, transform.position);

        GameManager.Instance.UpdateCoins(Value);

        //CoinParticleSystem.transform.SetParent(null);
        //CoinParticleSystem.Play();

        Destroy(gameObject);
    }
        
}
