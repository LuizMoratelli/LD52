using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
    [SerializeField] private int maxAmountOfEachItem = 99;
    [SerializeField] private TextMeshProUGUI flowersText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Start()
    {
        GameManager.instance.OnScoreChang += UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{GameManager.instance.Score}";
    }

    private void OnDisable()
    {
        GameManager.instance.OnScoreChang -= UpdateScoreText;

    }
    public void Add(InventoryItemType type, int quantity)
    {
        var item = items.FirstOrDefault(i => i.type == type);

        var needsToUpdate = item.quantity > 0;

        if (needsToUpdate)
        {
            items.Remove(item);
            item.quantity = Math.Min(item.quantity + quantity, maxAmountOfEachItem);
            items.Add(item);
        }
        else
        {
            items.Add(new InventoryItem(type, Math.Min(quantity, maxAmountOfEachItem)));
        }

        flowersText.text = $"{item.quantity} / {maxAmountOfEachItem}";

    }

    public void Remove(InventoryItemType type, int quantity)
    {
        var item = items.FirstOrDefault(i => i.type == type);

        var hasQuantity = item.quantity >= quantity;

        if (hasQuantity)
        {
            items.Remove(item);
            item.quantity -= quantity;

            flowersText.text = $"{item.quantity} / {maxAmountOfEachItem}";
            if (item.quantity == 0) return;

            items.Add(item);
        }
        else
        {
            throw new Exception("Insufficient item");
        }

    }
}

[Serializable]
public struct InventoryItem
{
    public InventoryItemType type;
    public int quantity;

    public InventoryItem(InventoryItemType type, int quantity)
    {
        this.type = type;
        this.quantity = quantity;
    }

}

public enum InventoryItemType
{
    BLUE_FLOWER
}
