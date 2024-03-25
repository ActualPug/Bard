using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerProjectile") {
            this.transform.Find("Body").GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(DeathAudio());
        }
    }

    IEnumerator DeathAudio() {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.time = 0.3f;
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
