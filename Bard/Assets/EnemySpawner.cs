using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    [SerializeField] Bard bard;
    [SerializeField] GameObject fireElementalPrefab;
    [SerializeField] Sprite fireElementalSprite;
    [Tooltip("X and Y are spawn position, Z is the weight of that spawn occuring.")]
    [SerializeField] private List<Vector3> spawnLocations;
    [Tooltip("The lower this number, the harder the game will get when difficulty is increased.")]
    [SerializeField] float difficultyModifier = 0.5f;
    [Tooltip("The amount of seconds to pass before difficulty is increased.")]
    [SerializeField] float difficultyFrequency = 15f;
    float waitTimeInSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
        ModifyDifficulty();
    }

    void SpawnEnemies() {
        StartCoroutine(SpawnEnemiesRoutine());
        IEnumerator SpawnEnemiesRoutine() {
            while(!bard.gameOver) {
                yield return new WaitForSeconds(waitTimeInSeconds);
                if (!bard.gameOver) {
                    SpawnEnemyRandom();
                }
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

    void ModifyDifficulty() {
        StartCoroutine(ModifyDifficultyRoutine());
        IEnumerator ModifyDifficultyRoutine() {
            while(!bard.gameOver) {
                yield return new WaitForSeconds(difficultyFrequency);
                if(!bard.gameOver) {
                    waitTimeInSeconds *= difficultyModifier;
                    Debug.Log("Increased difficulty");
                }
            }
        }
    }
}
