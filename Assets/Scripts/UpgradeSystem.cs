using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ErrorType
{
    NO_RESOURCE,
    OTHER
}

public class UpgradeSystem : MonoBehaviour
{
    private Inventory inventoryComponent;
    private Movement movementComponent;
    private HealthController playerHealthController;
    private HealthController houseHealthController;

    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] private GameObject terrainSpots;
    [SerializeField] private GameObject scythePrefab;
    [SerializeField] private List<GameObject> scythes;

    [SerializeField] private GameObject TerrainPanel;
    [SerializeField] private GameObject ScythePanel;
    [SerializeField] private GameObject SpeedPanel;
    [SerializeField] private GameObject HealthPanel;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color errorColor;
    [SerializeField] private Color warningColor;


    void Start()
    {
        inventoryComponent = gameObject.GetComponent<Inventory>();
        movementComponent = gameObject.GetComponent<Movement>();
        playerHealthController = gameObject.GetComponent<HealthController>();
        houseHealthController = GameObject.FindGameObjectWithTag("House").GetComponent<HealthController>();
    }

    private bool checkIfAlreadyExistsInPosition(Vector2 pos)
    {
        for (int i = 0; i < terrainSpots.transform.childCount; i++)
        {
            if (terrainSpots.transform.GetChild(i).name == $"{pos.x}, {pos.y}")
            {
                return true;
            }
        }

        return false;
    }

    private void ErrorOnPanel(GameObject panel, ErrorType type)
    {
        switch (type)
        {
            case ErrorType.NO_RESOURCE:
                StartCoroutine(FlashColor(panel, errorColor));
                break;
            case ErrorType.OTHER:
                StartCoroutine(FlashColor(panel, warningColor));
                break;
        }
    }

    private IEnumerator FlashColor(GameObject panel, Color color)
    {
        panel.GetComponent<Image>().color = color;
        yield return new WaitForSeconds(0.2f);
        panel.GetComponent<Image>().color = defaultColor;
        yield return new WaitForSeconds(0.2f);
        panel.GetComponent<Image>().color = color;
        yield return new WaitForSeconds(0.2f);
        panel.GetComponent<Image>().color = defaultColor;
        yield return new WaitForSeconds(0.2f);
        panel.GetComponent<Image>().color = color;
        yield return new WaitForSeconds(0.2f);
        panel.GetComponent<Image>().color = defaultColor;
    }

    // Precisa bloquear os limites de bounds
    public void BuyTerrain()
    {
        var position = new Vector2((float)Math.Floor(transform.position.x), (float)Math.Floor(transform.position.y));

        // Aqui pode ser melhorado pra explicar pro usuário
        if (checkIfAlreadyExistsInPosition(position))
        {
            ErrorOnPanel(TerrainPanel, ErrorType.OTHER);
            return;
        }

        var canBuy = TryToPurchase(4);

        // Aqui pode ser melhorado pra explicar pro usuário
        if (!canBuy)
        {
            ErrorOnPanel(TerrainPanel, ErrorType.NO_RESOURCE);
            return;
        }

        var newTerrain = Instantiate(terrainPrefab, terrainSpots.transform);
        newTerrain.transform.position = position + new Vector2(0.5f, -0.5f);
        newTerrain.name = $"{position.x}, {position.y}";
    }

    public void BuyScythe()
    {
        if (scythes.Count() == 10)
        {
            ErrorOnPanel(ScythePanel, ErrorType.OTHER);
            return;
        }

        var canBuy = TryToPurchase(30);

        if (!canBuy)
        {
            ErrorOnPanel(ScythePanel, ErrorType.NO_RESOURCE);
            return;
        }

        var newScythe = Instantiate(scythePrefab, transform);
        newScythe.GetComponent<Rotation>().pivot = transform;
        scythes.Add(newScythe);

        for (int i = 0; i < scythes.Count(); i++)
        {
            scythes[i].GetComponent<Rotation>().CurrentRotation = 360.0f / scythes.Count() * i;
        }

    }

    public void BuySpeed()
    {
        if (movementComponent.Speed >= 15)
        {
            ErrorOnPanel(SpeedPanel, ErrorType.OTHER);
            return;
        }

        if (!TryToPurchase(10))
        {
            ErrorOnPanel(SpeedPanel, ErrorType.NO_RESOURCE);
            return;
        }

        movementComponent.Speed += 0.25f;
    }
    public void BuyRecoverHealth()
    {
        if (!TryToPurchase(50))
        {
            ErrorOnPanel(HealthPanel, ErrorType.NO_RESOURCE);
            return;
        }

        playerHealthController.RecoverAllHealth();
        houseHealthController.RecoverAllHealth();
    }

    bool TryToPurchase(int quantity)
    {
        try
        {
            inventoryComponent.Remove(InventoryItemType.BLUE_FLOWER, quantity);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
