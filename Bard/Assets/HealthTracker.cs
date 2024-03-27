using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    [SerializeField] GameObject heartIconPrefab;
    List<GameObject> heartIcons = new List<GameObject>();
    int BardHealth;
    // Start is called before the first frame update
    void Start()
    {
        InitializeHeartIcons();
        BardHealth = bardCreature.health;
    }

    // Update is called once per frame

    public void ReduceHealth() {
        int index = bardCreature.health / 2;
        
        if (bardCreature.health % 2 == 1) {
            heartIcons[index].GetComponent<HeartIcon>().ChangeHeartToHalf();
        }
        else {
            heartIcons[index].GetComponent<HeartIcon>().ChangeHeartToNone();
        }
    }

    void InitializeHeartIcons() {
        GameObject newObject;
        int numHearts = bardCreature.health / 2;
        float xPosition = 21.5f;
        for (int i = 0; i < numHearts; i++) {
            newObject = Instantiate(heartIconPrefab, new Vector3(xPosition, 11.5f, 1), Quaternion.identity);
            xPosition -= 2;
            heartIcons.Add(newObject);
        }

        if (bardCreature.health % 2 != 0) {
            newObject = Instantiate(heartIconPrefab, new Vector3(xPosition, 11.5f, 1), Quaternion.identity);
            newObject.GetComponent<HeartIcon>().ChangeHeartToHalf();
            heartIcons.Add(newObject);
        }
    }
}
