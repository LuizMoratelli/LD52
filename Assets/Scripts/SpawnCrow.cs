using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrow : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxSpawnQuantity;
    [SerializeField] AnimationCurve quantityByWave;
    [SerializeField] private int quantityOfWaves;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    private int currentWave;
    void Start()
    {

    }

    [ContextMenu("SPAWN")]
    public void Spawn(int level = 1)
    {
        currentWave++;
        for (int i = 0; i < quantityInCurrentWaveToSpawn(); i++)
        {
            var randomX = Random.Range(-xOffset, xOffset);
            var randomY = Random.Range(-yOffset, yOffset);

            var enemy = Instantiate(enemyPrefab, transform);
            enemy.GetComponent<HealthController>()?.SetMaxHealth(3 + level * 2);
            enemy.transform.localPosition = new Vector2(randomX, randomY);
            float scale = level * 0.05f + 1;
            enemy.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    int quantityInCurrentWaveToSpawn()
    {
        if (currentWave > quantityOfWaves) return 0;
        return (int)(quantityByWave.Evaluate((float)currentWave / quantityOfWaves) * maxSpawnQuantity);
    }
}
