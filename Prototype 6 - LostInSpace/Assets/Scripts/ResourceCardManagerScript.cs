using System;
using System.Collections;
using System.Collections.Generic;
using Class;
using TMPro;
using UnityEngine;

public class ResourceCardManagerScript : MonoBehaviour
{
    public ResourceCardScript[] resourceCards;
    public RectTransform rectTransform;
    public RectTransform travelCostMain;
    public TextMeshProUGUI travelCostText;
    public bool useTravelCostText = true;
    private void Start()
    {
        HideAllResourceCards();
        rectTransform = GetComponent<RectTransform>();
    }
    public void HideAllResourceCards()
    {
        foreach (var resourceCardScript in resourceCards)
        {
            resourceCardScript.HideResourceCard();
        }
        if(useTravelCostText) travelCostMain.gameObject.SetActive(false);
    }
    public void SetResourceCards(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resourceCards[i].SetResourceCard(resources[i]);
        }
    }
    public void SetPosition(Vector2 screenPosition)
    {
        //Take the screenPosition and set the rectTransform position to it, from default pivot point of center
        //but keep the card in the screen, position the card in the opposite direction of the current corner if it would go off screen
        Vector2 targetPosition = screenPosition;
        if (screenPosition.x + rectTransform.rect.width > Screen.width)
        {
            targetPosition.x = screenPosition.x - (screenPosition.x + rectTransform.rect.width/2 -Screen.width);
        }
        if (screenPosition.y + rectTransform.rect.height > Screen.height)
        {
            targetPosition.y = screenPosition.y - (screenPosition.y + rectTransform.rect.height/2 - Screen.height);
        }
        if(screenPosition.x - rectTransform.rect.width < 0)
        {
            targetPosition.x = screenPosition.x + (rectTransform.rect.width/2 - screenPosition.x);
        }
        if(screenPosition.y - rectTransform.rect.height < 0)
        {
            targetPosition.y = screenPosition.y + (rectTransform.rect.height/2 - screenPosition.y);
        }
        
        rectTransform.position = targetPosition;
    }

    public void SetTravelCostText(int travelCost)
    {
        travelCostText.text = travelCost.ToString();
        travelCostMain.gameObject.SetActive(true);
    }
    

}
