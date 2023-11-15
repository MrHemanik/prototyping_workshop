using System.Linq;
using Class;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlayerManager playerManager;
    public ResourceCardManagerScript upgradePriceCardManagerScript;
    public ResourceCardManagerScript resourceCardManagerScript;
    public UpgradeType upgradeType;
    public TextMeshProUGUI currentUpgradeLevel;
    private int _upgradeLevel = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradePriceCardManagerScript.travelCostMain.gameObject.SetActive(true);
        var currentUpgradeLevel = playerManager.UpgradeLevels[(int)upgradeType];
        Upgrade upgrade = Upgrades.FindUpgradesByUpgradeType(upgradeType);
        var maxUpgradeLevel = upgrade.resources.Length;
        Debug.Log(currentUpgradeLevel+" "+maxUpgradeLevel);
        if (currentUpgradeLevel >= maxUpgradeLevel)
        {
            upgradePriceCardManagerScript.HideAllResourceCards();
            upgradePriceCardManagerScript.travelCostText.text = (upgradeType == UpgradeType.FrontalCamera) ? "Click on the ship to change perspective":"Max Level";
            upgradePriceCardManagerScript.travelCostMain.gameObject.SetActive(true);
            
            return;
        }
        upgradePriceCardManagerScript.travelCostText.text = "Price for Upgrade:";
        upgradePriceCardManagerScript.SetResourceCards(upgrade.resources[playerManager.UpgradeLevels[(int)upgradeType]].ToArray());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        upgradePriceCardManagerScript.HideAllResourceCards();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (playerManager.BuyUpgrade(upgradeType))
        {
            _upgradeLevel++;
            currentUpgradeLevel.text = _upgradeLevel.ToString();
            resourceCardManagerScript.SetResourceCards(playerManager.Resources.ToArray());
            OnPointerEnter(eventData);
        }
        else
        {
            upgradePriceCardManagerScript.travelCostText.text = "Not enough resources";
        }
        
    }
}