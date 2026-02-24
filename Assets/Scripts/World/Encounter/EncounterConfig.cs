namespace Monsters.World.Encounter
{
    public readonly struct EncounterConfig
    {
        public EncounterConfig(float biomeEncounterChance)
        {
            BiomeEncounterChance = biomeEncounterChance < 0f ? 0f : biomeEncounterChance > 1f ? 1f : biomeEncounterChance;
        }

        public float BiomeEncounterChance { get; }
    }
}
