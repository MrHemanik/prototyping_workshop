using System;
using System.Linq;
using Class;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlanetGenerator : MonoBehaviour
{
        public GameObject planetPrefab; 
        public PlanetUIManager planetUiManager;
        public PlayerManager playerManager;
        public AudioManager audioManager;
        public int solarSystemRadius = 500;
        private float _planetSizesTotalWeight;
        private (float, float)[] _planetSizes = new (float, float)[]
        {
                (0.5f, 1f),
                (0.6f, 2f),
                (0.7f, 3f),
                (0.8f, 4f),
                (0.9f, 5f),
                (1f, 6f),
                (1.1f, 5f),
                (1.2f, 4f),
                (1.3f, 3f),
                (1.4f, 2f),
                (1.5f, 1f),
                (2f, 0.1f),
                (3f, 0.05f),
                (5f, 0.01f),
                (5f, 0.01f),
        };

        private void Start()
        {
                _planetSizesTotalWeight = 0;
                for (var i = 0; i < _planetSizes.Length; i++)
                {
                        _planetSizesTotalWeight+=_planetSizes[i].Item2;
                }

                GenerateSolarSystem();
        }

        public void GenerateSolarSystem()
        {
                for (int i = 0; i < 7000; i++)
                {
                        GeneratePlanetInstance();
                }
        }

        
        public void GeneratePlanetInstance()
        {
                float randomSizeValue = Random.Range(0f, _planetSizesTotalWeight);
                float planetSize = _planetSizes.First(tuple => (randomSizeValue -= tuple.Item2) <= 0).Item1;

                int resourceAmount = Random.Range(1, 3);
                Resource[] resources = new Resource[resourceAmount];
                for (int i = 0; i < resourceAmount; i++)
                {
                        resources[i] = new Resource(GetRandomResourceType(), (int)(Random.Range(2.0f, 10.0f)*planetSize));
                }
                resources[0].Amount *= 3; // Main Source should be more abundant
                GameObject planet = Instantiate(planetPrefab,
                        new Vector3(Random.Range(-solarSystemRadius, solarSystemRadius), 0,
                                Random.Range(-solarSystemRadius, solarSystemRadius)),
                        Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),
                        transform);
                planet.GetComponent<Planet>().SetPlanetData(planetSize, resources, planetUiManager, playerManager, audioManager);
                planet.transform.position += new Vector3(Random.Range(-solarSystemRadius,solarSystemRadius), 0, Random.Range(-solarSystemRadius,solarSystemRadius));
        }
        
        public ResourceType GetRandomResourceType()
        {
                float totalWeight = Resource.resourceTypeWeights.Sum();
                float randomValue = Random.Range(0f, totalWeight);
                return (ResourceType) Resource.resourceTypeWeights.Select((w, i) => new { Weight = w, Index = i })
                        .First(x => (randomValue -= x.Weight) <= 0).Index;
        }
}