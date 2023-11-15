using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public ResourceCardManagerScript resourceCardManagerScript;
        public PlayerManager playerManager;
        public GameObject resourcesInfoText;
        public ResourceCardManagerScript upgradePriceCardManagerScript;
        // Upgrade

        public UpgradeUIManager upgradeUiManager;
        public GameObject upgradeInfoText;
        public Image upgradeUIBackground;
        public GameObject[] upgradeButtons;
        
        //Frontal Camera
        public Camera frontalCamera;
        public bool isFrontalCameraOn = false;
        
        public int windowState = 0;

        private void Start()
        {
            upgradeUIBackground = upgradeUiManager.GetComponentInParent<Image>();
            DeactivateUpgradeUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
             OnPointerEnterHandler();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
            OnPointerExitHandler();
        }

        public void OnPointerEnterHandler()
        {
            windowState++;
            if (windowState > 1) return;
            
            resourceCardManagerScript.SetResourceCards(playerManager.Resources.ToArray());
            ActivateUpgradeUI();
        }
        public void OnPointerExitHandler()
        {
            windowState--;
            if (windowState > 0) return;
            ClosePlayerUI();
        }

        private void ClosePlayerUI()
        {
            resourceCardManagerScript.HideAllResourceCards();
            upgradePriceCardManagerScript.HideAllResourceCards();
            DeactivateUpgradeUI();
        }
        
        
        
        
        // Upgrade
        public void ActivateUpgradeUI()
        {
            upgradeUIBackground.raycastTarget = true;
            resourcesInfoText.SetActive(true);
            upgradeInfoText.SetActive(true);
            foreach (var upgradeButton in upgradeButtons)
            {
                upgradeButton.SetActive(true);
            }
        }
        public void DeactivateUpgradeUI()
        {
            upgradeUIBackground.raycastTarget = false;
            resourcesInfoText.SetActive(false);
            upgradeInfoText.SetActive(false);
            foreach (var upgradeButton in upgradeButtons)
            {
                upgradeButton.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var isFrontalCameraAllowed = playerManager.isFrontalCameraAllowed;
            if (isFrontalCameraAllowed)
            {
                isFrontalCameraOn = !isFrontalCameraOn;
                frontalCamera.gameObject.SetActive(isFrontalCameraOn);
                ClosePlayerUI();
            }
        }
    }
