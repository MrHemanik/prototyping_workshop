using System.Collections.Generic;
using Class;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void OnReset(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SceneManager.LoadScene(0);
    }

    public void OnResourceCheat(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        List<Resource> Resources = new List<Resource>(){new Resource(ResourceType.Biofuel,999),new Resource(ResourceType.Cryothem,999),new Resource(ResourceType.Hexafluxene,999),new Resource(ResourceType.Lumonium,999),new Resource(ResourceType.PlasmaResin,999)};
        FindObjectOfType<PlayerManager>().AddResources(Resources.ToArray());
    }
}
