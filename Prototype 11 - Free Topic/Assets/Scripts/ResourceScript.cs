using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ResourceType
{
    Wood,
    Stone,
    Iron,
    Gold
    
}
//Make Resource show up in inspector
[System.Serializable]
public class Resource
{
    public ResourceType type;
    public int amount;
    public static string GetResourceName(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                return "Wood";
            case ResourceType.Stone:
                return "Stone";
            case ResourceType.Iron:
                return "Iron";
            case ResourceType.Gold:
                return "Gold";
            default:
                return "Unknown";
        }
    }
}
//Make Resource show up in inspector
public class ResourceScript : MonoBehaviour
{
    public int health;
    public List<Resource> resources;
    public Animator _animator;
    

    public (bool, List<Resource>) HarvestResource()
    {
        health--;
        _animator.SetTrigger("Harvest");
        return health <= 0 ? (true, resources) : (false, null);
    }
}