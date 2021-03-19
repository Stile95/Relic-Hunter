using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public AudioClip PickupAudioClip;
    public ParticleSystem ChestParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision )
    {
        if (collision.tag != "Player" && PlayerCombat.isDead)

            return;

        AudioManager.instance.Play("GoldChest");

        GameManager.Instance.UpdateCoins(3);

        ChestParticleSystem.transform.SetParent(null);
        ChestParticleSystem.Play();

        Destroy(gameObject);
    }


}
