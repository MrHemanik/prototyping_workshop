using System.Collections.Generic;
using Class;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public List<Resource> Resources = new List<Resource>(){new Resource(ResourceType.Biofuel,100)};
    public int[] UpgradeLevels = { 0, 0, 0, 0 };
    private int baseVisionRange = 10;
    private int baseFuelEfficiency = 10;
    public int fuelEfficiency = 10;
    public bool isFrontalCameraAllowed = false;


    public Camera topDownCamera;

    
    public UnityEvent<Resource[], Vector3> onPlanetClick;
    public UnityEvent<float, Vector3> onPlanetHoverEnter;

    public Vector3 targetPosition = Vector3.zero;
    public Quaternion targetRotation = Quaternion.identity;
    public Transform playerRotationBody;
    
    private SphereCollider _collider;
        
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
        onPlanetClick.AddListener(OnPlanetClickHandler);
        onPlanetHoverEnter.AddListener(OnPlanetHoverEnterHandler);
    }
    
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        playerRotationBody.rotation = Quaternion.Lerp(playerRotationBody.rotation, targetRotation, 0.1f);
    }
    
    private void OnPlanetClickHandler(Resource[] resources, Vector3 planetPosition)
    {
        targetPosition = planetPosition;
        int travelCost = (int)((planetPosition - transform.position).magnitude / fuelEfficiency * 10);
        RemoveResource(new Resource(ResourceType.Biofuel, travelCost));
        if (!CheckResource(new Resource(ResourceType.Biofuel, 0))) SceneManager.LoadScene(1);
        AddResources(resources);
        
    }
    private void OnPlanetHoverEnterHandler(float size,Vector3 position)
    {
        targetRotation = Quaternion.LookRotation(position-transform.position,Vector3.up);
        Debug.Log("PlayerUI: OnPlanetHoverEnterHandler");
    }
    
    
    
    
    
    public void AddResources(Resource[] addResources)
    {
        foreach (var resource in addResources)
        {
            AddResource(resource);
        }
    }
    private void AddResource(Resource addResource)
    {
        //Search for resourceType entry in array and add the amount to it, if it doesnt exist create an entry
        foreach (var resource in Resources)
        {
            if (resource.rt == addResource.rt)
            {
                resource.Amount += addResource.Amount;
                // addResource.Amount = 0; Do on planet self
                return;
            }
        }
        //Add a copy of the resource
        Resources.Add(new Resource(addResource.rt, (int) addResource.Amount));
    }
    private void RemoveResource(Resource removeResource)
    {
        //Search for resourceType entry in array and remove the amount from it, if it doesnt exist create an entry
        foreach (var resource in Resources)
        {
            if (resource.rt == removeResource.rt)
            {
                resource.Amount -= removeResource.Amount;
                return;
            }
        }
    }
    private void RemoveResources(Resource[] removeResources)
    {
        foreach (var resource in removeResources)
        {
            RemoveResource(resource);
        }
    }
    private bool CheckResource(Resource checkResource)
    {
        //Search for resourceType entry in array and check if the amount is greater than or equal to the amount in the checkResource
        foreach (var resource in Resources)
        {
            if (resource.rt == checkResource.rt)
            {
                return resource.Amount >= checkResource.Amount;
            }
        }
        return false;
    }
    private bool CheckResources(Resource[] checkResources)
    {
            
        foreach (var checkResource in checkResources)
        {
            if (!CheckResource(checkResource)) return false;
        }
        return true;
    }
        
    public bool BuyUpgrade(UpgradeType upgradeType)
    {
        Upgrade upgrade = Upgrades.FindUpgradesByUpgradeType(upgradeType);
        var currentUpgradeLevel = UpgradeLevels[(int)upgradeType];
        var maxUpgradeLevel = upgrade.resources.Length;
        if (currentUpgradeLevel >= maxUpgradeLevel) return false;
        Resource[] upgradePrice = upgrade.resources[currentUpgradeLevel];
        if (CheckResources(upgradePrice))
        {
            RemoveResources(upgradePrice);
            UpgradeLevels[(int)upgrade.ut]++;
            ActivateUpgrades();
            return true;
        }
        return false;
    }

    private void ActivateUpgrades()
    {
        topDownCamera.orthographicSize = baseVisionRange + 2*UpgradeLevels[(int)UpgradeType.VisionRange];
        _collider.radius = (baseVisionRange+2.0f + 2*UpgradeLevels[(int)UpgradeType.VisionRange])/10f;
        fuelEfficiency = baseFuelEfficiency + 2*UpgradeLevels[(int)UpgradeType.FuelEfficiency];
        isFrontalCameraAllowed = UpgradeLevels[(int)UpgradeType.FrontalCamera] > 0;
        if (UpgradeLevels[(int)UpgradeType.FTLDrive] > 0) SceneManager.LoadScene(2);
    }


    
}