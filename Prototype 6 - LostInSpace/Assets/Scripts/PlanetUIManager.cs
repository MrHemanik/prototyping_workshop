using Class;
using UnityEngine;
using UnityEngine.Events;

public class PlanetUIManager : MonoBehaviour
{
        public UnityEvent<Resource[], Vector2, Vector3, int> onPlanetHoverEnter;
        public UnityEvent onPlanetHoverExit;
        public ResourceCardManagerScript resourceCardManagerScript;
        public Transform playerTransform;
        private void Start()
        {
                onPlanetHoverEnter.AddListener(OnPlanetHoverEnterHandler);
                onPlanetHoverExit.AddListener(OnPlanetHoverExitHandler);
        }

        private void OnPlanetHoverEnterHandler(Resource[] resources, Vector2 screenPosition, Vector3 planetPosition, int fuelEfficiency)
        {
                foreach (var resource in resources)
                {
                        Debug.Log(resource.rt+" "+resource.Amount);
                }
                resourceCardManagerScript.SetPosition(screenPosition);
                resourceCardManagerScript.SetResourceCards(resources);
                //Calculate the travel cost based on the fuel cost of the ship and the distance to the planet
                int travelCost = (int)(planetPosition-playerTransform.position).magnitude * 10 / fuelEfficiency;
                resourceCardManagerScript.SetTravelCostText(travelCost);

        }
        private void OnPlanetHoverExitHandler()
        {
                resourceCardManagerScript.HideAllResourceCards();
        }

}