using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerUIManager playerUIManager;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        playerUIManager.OnPointerEnterHandler();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerUIManager.OnPointerExitHandler();
    }
}
