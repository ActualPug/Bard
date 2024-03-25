using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            Destroy(this.gameObject);
        } else if(other.gameObject.tag == "Barrier") {
            Destroy(this.gameObject);
        }
    }
}
