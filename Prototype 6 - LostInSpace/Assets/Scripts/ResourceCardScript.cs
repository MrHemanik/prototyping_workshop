
using Class;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCardScript : MonoBehaviour
{
    public Image resourceImage;
    public Image resourceBorder;
    public TextMeshProUGUI resourceName;
    public TextMeshProUGUI resourceAmount;
    
    public void SetResourceCard(Resource resource)
    {
        Color resoureColor = Resource.GetResourceColor(resource.rt);
        resourceImage.color = resoureColor;
        resourceBorder.color = resoureColor;
        resourceName.text = Resource.GetResourceName(resource.rt);
        resourceName.color = resoureColor;
        resourceAmount.text = resource.Amount.ToString();
        resourceAmount.color = resoureColor;
        gameObject.SetActive(true);
    }
    public void HideResourceCard()
    {
        gameObject.SetActive(false);
    }
}
