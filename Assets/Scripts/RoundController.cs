using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public bool IsFreezed = false;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject birdSystem;
    [SerializeField] private float waitBetweenRounds = 60.0f;
    [SerializeField] private float waitBetweenSpawners = 10.0f;
    [SerializeField] private float waitBetweenRefreshPlants = 20.0f;
    public int CurrentRound { get; private set; }
    [SerializeField] private int quantityOfRounds = 10;
    [SerializeField] private TextMeshProUGUI roundText;

    void Start()
    {
        StartCoroutine(RefreshPlants());
        StartCoroutine(Round());
    }

    IEnumerator Round()
    {
        //if (++CurrentRound <= quantityOfRounds)
        //{
        CurrentRound++;
        StartCoroutine(UpdateRoundText());
        for (int i = 0; i < spawners.Count(); i++)
        {
            spawners[i].GetComponent<SpawnCrow>().Spawn(CurrentRound / 3);

            if (i + 1 < spawners.Count())
                yield return new WaitForSeconds(waitBetweenSpawners);

        }
        // }

        yield return new WaitForSeconds(waitBetweenRounds);
        StartCoroutine(Round());
    }

    IEnumerator UpdateRoundText()
    {
        roundText.text = $"{CurrentRound}";

        if (CurrentRound % 3 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    roundText.fontSize = 20 + j;
                    yield return new WaitForSeconds(0.2f);

                }

                for (int j = 0; j < (i == 2 ? 10 : 15); j++)
                {
                    roundText.fontSize = 30 - j;
                    yield return new WaitForSeconds(0.2f);

                }
            }

        }
    }

    IEnumerator RefreshPlants()
    {
        birdSystem.GetComponent<BirdSystem>().Plant();
        yield return new WaitForSeconds(waitBetweenRefreshPlants);

        //if (CurrentRound <= quantityOfRounds)
        //{
        //}

        StartCoroutine(RefreshPlants());
    }
}
