using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Vector2 tileOffset = new(0.5f, -0.5f);
    [SerializeField] private PlantSO plantStages;
    [SerializeField] private InventoryItemType invType;
    private int currentStageIndex;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject harvestParticle;

    void UpdateVisuals()
    {
        spriteRenderer.sprite = plantStages.Stages[currentStageIndex].SpriteStage;
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(NextStage());
    }

    IEnumerator NextStage()
    {
        UpdateVisuals();
        yield return new WaitForSeconds(plantStages.Stages[currentStageIndex].TimeToNextStage);

        if (++currentStageIndex < plantStages.Stages.Count())
        {
            StartCoroutine(NextStage());
        }
    }

    public bool couldHaverst()
    {
        Debug.Log(currentStageIndex);
        Debug.Log(plantStages.Stages.Count());
        return currentStageIndex == plantStages.Stages.Count();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon") && couldHaverst())
        {

            var invetory = FindObjectOfType<Inventory>();
            invetory.Add(invType, 1);
            GameManager.instance.IncreaseScore(5);
            Instantiate(harvestParticle, transform.position, new Quaternion());
            GameManager.instance.PlaySFX("harvest");


            Destroy(this.gameObject);
        }

        /* else if (collision.gameObject.CompareTag("Enemy"))
         {
             Destroy(this.gameObject);
         }*/
    }
}
