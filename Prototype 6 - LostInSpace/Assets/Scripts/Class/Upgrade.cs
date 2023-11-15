namespace Class
{
    public enum UpgradeType
    {
        VisionRange,
        FuelEfficiency,
        FrontalCamera,
        FTLDrive,
    }

    public class Upgrade
    {
        public UpgradeType ut;

        // Have a list of resources that are required to unlock the upgrade, as an array of resources for each level
        public Resource[][] resources;

        public Upgrade(UpgradeType utIn, Resource[][] resourcesIn)
        {
            ut = utIn;
            resources = resourcesIn;
        }
    }

    public static class Upgrades
    {
        public static Upgrade FindUpgradesByUpgradeType(UpgradeType ut)
        {
            //returns the upgrade of the given type
            switch (ut)
            {
                case UpgradeType.VisionRange:
                    return visionRangeUpgrades;
                case UpgradeType.FuelEfficiency:
                    return fuelEfficiencyUpgrades;
                case UpgradeType.FrontalCamera:
                    return frontalCameraUpgrades;
                case UpgradeType.FTLDrive:
                    return ftlDriveUpgrades;
                default:
                    return null;
            }
        }

        public static Upgrade visionRangeUpgrades = new Upgrade(UpgradeType.VisionRange,
            new Resource[][]
            {
                new Resource[] { new Resource(ResourceType.Lumonium, 50) },
                new Resource[] { new Resource(ResourceType.Lumonium, 100) },
                new Resource[] { new Resource(ResourceType.Lumonium, 200) },
                new Resource[] { new Resource(ResourceType.Lumonium, 300) },
                new Resource[] { new Resource(ResourceType.Lumonium, 350) },
                new Resource[] { new Resource(ResourceType.Lumonium, 450) },
                new Resource[] { new Resource(ResourceType.Lumonium, 500) },
                new Resource[] { new Resource(ResourceType.Lumonium, 550) },
            }
        );

        public static Upgrade fuelEfficiencyUpgrades = new Upgrade(UpgradeType.FuelEfficiency,
            new Resource[][]
            {
                new Resource[] { new Resource(ResourceType.Hexafluxene, 50) },
                new Resource[] { new Resource(ResourceType.Hexafluxene, 100) },
                new Resource[]
                    { new Resource(ResourceType.Hexafluxene, 200), new Resource(ResourceType.Cryothem, 50) },
                new Resource[]
                    { new Resource(ResourceType.Hexafluxene, 300), new Resource(ResourceType.Cryothem, 150) },
                new Resource[]
                    { new Resource(ResourceType.Hexafluxene, 400), new Resource(ResourceType.Cryothem, 200) },
                new Resource[]
                    { new Resource(ResourceType.Hexafluxene, 500), new Resource(ResourceType.Cryothem, 400) },
            }
        );

        public static Upgrade frontalCameraUpgrades = new Upgrade(UpgradeType.FrontalCamera,
            new Resource[][]
            {
                //Level 1
                new Resource[]
                {
                    new Resource(ResourceType.Lumonium, 100), new Resource(ResourceType.Hexafluxene, 100),
                    new Resource(ResourceType.Cryothem, 100)
                },
            }
        );

        public static Upgrade ftlDriveUpgrades = new Upgrade(UpgradeType.FTLDrive,
            new Resource[][]
            {
                //Level 1
                new Resource[]
                {
                    new Resource(ResourceType.PlasmaResin, 100), new Resource(ResourceType.Lumonium, 100),
                    new Resource(ResourceType.Hexafluxene, 100), new Resource(ResourceType.Cryothem, 100)
                },
            }
        );
    }
}