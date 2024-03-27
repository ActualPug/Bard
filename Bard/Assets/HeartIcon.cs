using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartIcon : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    [SerializeField] Sprite fullHeartSprite;
    [SerializeField] Sprite halfHeartSprite;
    // Start is called before the first frame update

    public void ChangeHeartToFull() {
        this.GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
    }

    public void ChangeHeartToHalf() {
        this.GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
    }

    public void ChangeHeartToNone() {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }
}
