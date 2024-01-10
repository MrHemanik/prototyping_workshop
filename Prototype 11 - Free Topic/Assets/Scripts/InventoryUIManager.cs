using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    private TextMeshProUGUI _inventoryItemsText;
    void Start()
    {
        _inventoryItemsText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateInventoryUI("");
    }

    // Update is called once per frame

    public void UpdateInventoryUI(string invItems)
    {
        _inventoryItemsText.text = invItems;
    }

    public void UpdateInventoryUI(List<Resource> invItems)
    {
        _inventoryItemsText.text = invItems.Aggregate("", (current, item) => current + (item.amount + " " + Resource.GetResourceName(item.type) + "\n"));
    }
}
