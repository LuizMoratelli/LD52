using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdSystem : MonoBehaviour
{
    [SerializeField] private Transform plantSpots;
    [SerializeField] private GameObject[] plantsPrefabs;

    void Start()
    {
        
    }

    [ContextMenu("Test")]
    public void Plant()
    {
        if (plantSpots.childCount == 0) return;

        for (int i = 0; i < plantSpots.childCount; i++) {
            var currentChild = plantSpots.GetChild(i);

            if (currentChild.childCount != 0) continue; 

            var randomPlantIndex = Random.Range(0, plantsPrefabs.Count() - 1);
            var randomPlant = plantsPrefabs[randomPlantIndex];

            Instantiate(randomPlant, currentChild.transform);

        }
    }
}
