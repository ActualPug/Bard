using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 0f;
    [SerializeField] int health = 3;

    [Header("Flavor")]
    public GameObject body;
    [SerializeField] private List<AnimationStateChanger> animationStateChangers;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;
    [SerializeField] CreatureSO creatureSO;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(creatureSO != null){
            creatureSO.health = health;
        }
    }

    public void MoveCreature(Vector3 direction)
    {

        Vector3 currentVelocity = Vector3.zero;

        rb.velocity = (currentVelocity) + (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        } else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }

        //set animation
        if(direction != Vector3.zero){
            foreach(AnimationStateChanger asc in animationStateChangers) {
                asc.ChangeAnimationState("Walk", speed);
            }
        }
        else {
            foreach(AnimationStateChanger asc in animationStateChangers) {
                asc.ChangeAnimationState("Idle");
            }
        }
    }

    public void MoveCreatureToward(Vector3 target) {
        Vector3 direction = target - transform.position;
        MoveCreature(direction.normalized);
    }

    public void Stop() {
        MoveCreature(Vector3.zero);
    }
}
