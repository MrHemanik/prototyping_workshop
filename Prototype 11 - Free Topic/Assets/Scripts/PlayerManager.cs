using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private NearestInteractableScript _nis;
    private float _harvestCooldown = 0.5f;
    private float _harvestCooldownTimer = 0f;
    private Animator _animator;
    private PriceUIManager _priceUIManager;
    private InventoryUIManager _inventoryUIManager;
    private AudioManager _audioManager;
    public List<Resource> _playerResources;
    void Start()
    {
        _nis = GetComponentInChildren<NearestInteractableScript>();
        _animator = GetComponent<Animator>();
        _priceUIManager = FindObjectOfType<PriceUIManager>();
        _inventoryUIManager = FindObjectOfType<InventoryUIManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    void Update()
    {
        if (_harvestCooldownTimer > 0)
        {
            _harvestCooldownTimer -= Time.deltaTime;
        }
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("pressInteraction");
        GameObject nearestInteractable = _nis.GetNearestInteractable();
        if (nearestInteractable == null) return;
        if (nearestInteractable.transform.CompareTag("Resource"))
        {
            if (_harvestCooldownTimer > 0) return;
            _harvestCooldownTimer = _harvestCooldown;
            _animator.SetTrigger("Attacking");
            _audioManager.Play("Attack");
            (bool isFullyHarvested, List<Resource> nodeResources) = nearestInteractable.transform.GetComponent<ResourceScript>().HarvestResource();
            if (isFullyHarvested)
            {
                //Add resources to player, stack if already present
                AddResources(nodeResources);
                _nis.everyInteractableInReach.Remove(nearestInteractable); //For as long as i insta delete
                Destroy(nearestInteractable);
            }
        }else if (nearestInteractable.transform.CompareTag("BuyArea"))
        {
            Debug.Log("BuyTry");
            var resourceCosts = nearestInteractable.GetComponent<BuyScript>().resourceCosts;
            if (!CheckResources(resourceCosts))
            {
                _priceUIManager.UpdatePriceUI("Not enough resources","");
                return;   
            }
            Debug.Log("Buy");
            _audioManager.Play("Buy");
            RemoveResources(resourceCosts);
            _nis.everyInteractableInReach.Remove(nearestInteractable);
            nearestInteractable.GetComponent<BuyScript>().Buy();
        }
    }

    private void AddResource(Resource resource)
    {
        bool hasResource = false;
        foreach (var playerResource in _playerResources)
        {
            if (playerResource.type == resource.type)
            {
                hasResource = true;
                playerResource.amount += resource.amount;
            }
        }
        if (!hasResource)
        {
            _playerResources.Add(resource);
        }
        _inventoryUIManager.UpdateInventoryUI(_playerResources);
    }
    private void AddResources(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            AddResource(resource);
        }
    }
    private void RemoveResource(Resource resource)
    {
        foreach (var playerResource in _playerResources)
        {
            if (playerResource.type == resource.type)
            {
                playerResource.amount -= resource.amount;
            }
        }
        _inventoryUIManager.UpdateInventoryUI(_playerResources);
    }
    private void RemoveResources(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            RemoveResource(resource);
        }
    }
    private bool CheckResources(List<Resource> resources)
    {
        int resourcesFound = 0;
        foreach (var resource in resources)
        {
            foreach (var playerResource in _playerResources)
            {
                if (playerResource.type == resource.type)
                {
                    if (playerResource.amount >= resource.amount)
                    {
                        resourcesFound++; //For when there are no resources saved in player
                    }
                    else return false;
                        
                }
            }
        }
        return resourcesFound >= resources.Count;
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SceneManager.LoadScene(0);
    }
    public void OnCheat(InputAction.CallbackContext context)
    {
        //Add every resource 100 times
        if (!context.started) return;
        AddResources((from ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType)) select new Resource { type = resourceType, amount = 100 }).ToList());
        
    }
}
