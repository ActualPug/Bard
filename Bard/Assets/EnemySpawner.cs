using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyToSpawnElement {
    public string enemyToSpawn;
    public float weight;

    public EnemyToSpawnElement(string enemyToSpawn, float weight) {
        this.enemyToSpawn = enemyToSpawn;
        this.weight = weight;
    }
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    [SerializeField] Bard bard;
    [SerializeField] GameObject fireElementalPrefab;
    [SerializeField] GameObject ghostPrefab;
    [Tooltip("X and Y are spawn position, Z is the weight of that spawn occuring.")]
    [SerializeField] private List<Vector3> spawnLocations;
    [SerializeField] private List<EnemyToSpawnElement> enemiesToSpawn;
    /*[Tooltip("The lower this number, the harder the game will get when difficulty is increased.")]
    [SerializeField] float difficultyModifier = 0.5f;
    [Tooltip("The amount of seconds to pass before difficulty is increased.")]
    [SerializeField] float difficultyFrequency = 15f;*/
    [Tooltip("The time in seconds between enemy spawns")]
    [SerializeField] float waitTimeInSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
        //ModifyDifficulty();
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
        GameObject enemyPrefab;
        Dictionary<Vector2, float> options = new Dictionary<Vector2, float>();
        foreach (Vector3 vec in spawnLocations) {
            options.Add(new Vector2(vec.x, vec.y), vec.z);
        }
        Vector2 spawnLocation = PickOption(options);
        Dictionary<string, float> enemy = new Dictionary<string, float>();
        foreach(EnemyToSpawnElement enemyToSpawnElement in enemiesToSpawn) {
            enemy.Add(enemyToSpawnElement.enemyToSpawn, enemyToSpawnElement.weight);
        }
        string enemyToSpawn = PickOption(enemy);
        switch (enemyToSpawn) {
            case "FireElemental":
                enemyPrefab = fireElementalPrefab;
                break;
            case "Ghost":
                enemyPrefab = ghostPrefab;
                break;
            default:
                enemyPrefab = fireElementalPrefab;
                break;
        }
        newObject = Instantiate(enemyPrefab, new Vector3(spawnLocation.x, spawnLocation.y, 0), Quaternion.identity);
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

    /*void ModifyDifficulty() {
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
    }*/
}
