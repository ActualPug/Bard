using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    [SerializeField] GameObject fireElementalPrefab;
    [SerializeField] Sprite fireElementalSprite;
    [Tooltip("X and Y are spawn position, Z is the weight of that spawn occuring.")]
    [SerializeField] private List<Vector3> spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies() {
        StartCoroutine(SpawnEnemiesRoutine());
        IEnumerator SpawnEnemiesRoutine() {
            while(true) {
                yield return new WaitForSeconds(1);
                SpawnEnemyRandom();
            }
        }
    }

    void SpawnEnemyRandom() {
        GameObject newObject;
        Dictionary<Vector2, float> options = new Dictionary<Vector2, float>();
        foreach (Vector3 vec in spawnLocations) {
            options.Add(new Vector2(vec.x, vec.y), vec.z);
        }
        Vector2 spawnLocation = PickOption(options);
        newObject = Instantiate(fireElementalPrefab, new Vector3(spawnLocation.x, spawnLocation.y, 0), Quaternion.identity);
        newObject.GetComponent<CreatureAI>().targetCreature = bardCreature;
    }

    T PickOption<T>(Dictionary<T, float> options) {
        float totalWeight = 0;
        foreach (var kvp in options) {
            totalWeight += kvp.Value;
        }

        float randomNumber = UnityEngine.Random.Range(0f,1f) * totalWeight;

        foreach (var kvp in options) {
            if (randomNumber < kvp.Value) {
                return kvp.Key;
            }

            randomNumber -= kvp.Value;
        }

        throw new InvalidOperationException();
    }
}
