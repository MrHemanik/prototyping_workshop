using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceUIManager : MonoBehaviour
{
    private TextMeshProUGUI _informationText;
    private TextMeshProUGUI _priceText;
    void Start()
    {
        _informationText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _priceText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdatePriceUI("","");
    }

    // Update is called once per frame

    public void UpdatePriceUI(string information, string price)
    {
        _informationText.text = information;
        _priceText.text = price;
    }
}
