using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState = "Idle";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimationState(string newState, float speed = 3) {
        animator.speed = speed;

        if(currentState == newState) {
            return;
        }
        currentState = newState;
        animator.Play(currentState);
    }
}
