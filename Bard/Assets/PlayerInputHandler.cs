using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] GameObject bard;
    [SerializeField] Creature playerCreature;
    [SerializeField] Canvas optionsCanvas;
    [SerializeField] float cooldownTime = 0.5f;
    ProjectileThrower projectileThrower;
    bool onCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        projectileThrower = playerCreature.GetComponent<ProjectileThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        Vector3 direction = playerCreature.transform.localPosition;
        bool shooting = false;

        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        if(Input.GetKey(KeyCode.W)){
            input.y += 1;
        }

        if(Input.GetKey(KeyCode.S)){
            input.y -= 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow)) {
            direction.x += -1;
            shooting = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction.x += 1;
            shooting = true;
        }

        if(Input.GetKey(KeyCode.UpArrow)){
            direction.y += 1;
            shooting = true;
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            direction.y -= 1;
            shooting = true;
        }

        if(Input.GetKeyDown(KeyCode.E)){
            projectileThrower.Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (optionsCanvas.enabled == true) {
                optionsCanvas.enabled = false;
                Time.timeScale = 1;
            }
            else {
                Time.timeScale = 0;
                optionsCanvas.enabled = true;
            }
        }

        if(shooting == true && !onCooldown) {
            projectileThrower.Launch(direction);
            StartCooldown();
        }

        if (bard.activeSelf) {
            playerCreature.MoveCreature(input);
        }
    }

    void StartCooldown() {
        StartCoroutine(StartCooldownRoutine());
        IEnumerator StartCooldownRoutine() {
            onCooldown = true;
            yield return new WaitForSeconds(cooldownTime);
            onCooldown = false;
        }
    }
}
