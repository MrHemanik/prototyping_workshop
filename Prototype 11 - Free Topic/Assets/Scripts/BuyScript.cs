using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    PriceUIManager _priceUIManager;
    public string buyText;
    public List<Resource> resourceCosts;
    void Start()
    {
        _priceUIManager = FindObjectOfType<PriceUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player")) return;
        var price = resourceCosts.Aggregate("", (current, resource) => current + (resource.amount + " " + Resource.GetResourceName(resource.type) + "\n"));
        _priceUIManager.UpdatePriceUI(buyText, price);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.transform.CompareTag("Player")) return;
        _priceUIManager.UpdatePriceUI("","");
    }

    public void Buy()
    {
        _priceUIManager.UpdatePriceUI("","");
        Destroy(GetComponent<BoxCollider>());
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
