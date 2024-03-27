using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage) {
        for(int i = 0; i < damage; i++) {
            bardCreature.health -= 1;
            this.GetComponent<HealthTracker>().ReduceHealth();
            if(bardCreature.health <= 0 ) {
                Death();
                return;
            }
        }
    }

    void Death() {
        gameOver = true;
    }
}
