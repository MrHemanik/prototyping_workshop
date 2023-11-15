

using UnityEngine;

namespace Class
{
    public enum ResourceType
    {
        Hexafluxene,
        Cryothem,
        Lumonium,
        PlasmaResin,
        Biofuel
    }

    public class Resource
    {
        public readonly ResourceType rt;
        public float Amount;
        public Resource(ResourceType rtIn, int amountIn)
        {
            rt = rtIn;
            Amount = amountIn;
        }
        
        public static float[] resourceTypeWeights =
        {
            4f,
            5f, 
            10f, 
            0.5f, 
            5f,
        };
        public static Color[] resourcePlanetColors =
        {
            new Color(1f, 0f, 0.25f),
            new Color(0f, 0.7f, 1f),
            new Color(1f, 0.78f, 0f),
            new Color(0f, 1f, 0.6f),
            new Color(0.48f, 0.24f, 0f),
        };
        
        public static Color GetResourceColor(ResourceType type)
        {
            return resourcePlanetColors[(int)type];
        }
        public static string[] resourceTypeNames =
        {
            "Hexafluxene",
            "Cryothem",
            "Lumonium",
            "Plasma Resin",
            "Biofuel",
        };
        public static string GetResourceName(ResourceType type)
        {
            return resourceTypeNames[(int)type];
        }

        
    }
    
}