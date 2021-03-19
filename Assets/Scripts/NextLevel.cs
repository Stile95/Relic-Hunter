using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public ParticleSystem RelicParticleEffect;

    private void Awake()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        else
        {
            AudioManager.instance.Play("ItemPickUp");
            //RelicParticleEffect.transform.SetParent(null);
            //RelicParticleEffect.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);

        }
       
    }

    


}
