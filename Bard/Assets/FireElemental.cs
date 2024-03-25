using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerProjectile") {
            Death();
        } else if (other.gameObject.tag == "Player") {
            Death();
            other.GetComponent<Creature>().health -= 1;
        }
    }

    IEnumerator DeathAudio() {
        //AudioSource audioSource = this.GetComponent<AudioSource>();
        while(audioSource.isPlaying) {
            yield return null;
        }
        Destroy(this.gameObject);
    }

    void Death() {
        audioSource.Play();
        this.transform.Find("Body").GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(DeathAudio());
    }
}
