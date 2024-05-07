using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Creature ghostCreature;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerProjectile") {
            ghostCreature.health -= 1;
            if (ghostCreature.health <= 0) {
                Death();
            }
        } else if (other.gameObject.tag == "Player") {
            Death();
            other.GetComponent<Bard>().TakeDamage(1);
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
