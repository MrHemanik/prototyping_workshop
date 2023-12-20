using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPlanetColorScript : MonoBehaviour
{
    public static Color[] resourcePlanetColors =
    {
        new Color(1f, 0f, 0.25f),
        new Color(0f, 0.7f, 1f),
        new Color(1f, 0.78f, 0f),
        new Color(0f, 1f, 0.6f),
        new Color(0.48f, 0.24f, 0f),
    };
    public static float[] resourceTypeWeights =
    {
        4f,
        5f, 
        10f, 
        0.5f, 
        5f,
    };
    public Color GetRandomResourceType()
    {
        float totalWeight = resourceTypeWeights.Sum();
        float randomValue = Random.Range(0f, totalWeight);
        return resourcePlanetColors[resourceTypeWeights.Select((w, i) => new { Weight = w, Index = i })
            .First(x => (randomValue -= x.Weight) <= 0).Index];
    }
    void Start()
    {
        bool isGoal = transform.CompareTag("Goal");
        GetComponent<MeshRenderer>().material.color = isGoal ? new Color(0.57f, 0.07f, 0.45f) : GetRandomResourceType();
        transform.rotation = Quaternion.Euler(isGoal ? 0: Random.Range(0, 360), isGoal ? 0: Random.Range(0, 360), Random.Range(0, 360));
    }

}
