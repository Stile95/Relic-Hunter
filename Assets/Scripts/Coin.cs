﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value = 10;
    public AudioClip PickupAudioClip;
    public ParticleSystem CoinParticleSystem;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")

            return;

      

        AudioSource.PlayClipAtPoint(PickupAudioClip, transform.position);

        GameManager.Instance.UpdateScore(Value);

            CoinParticleSystem.transform.SetParent(null);
            CoinParticleSystem.Play();
        

        Destroy(gameObject);

        

    }
    
        
}
