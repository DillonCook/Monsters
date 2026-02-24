using Monsters.World.Movement;
using UnityEngine;

namespace Monsters.World.Encounter
{
    public sealed class EncounterTriggerService
    {
        private readonly IEncounterRng _rng;
        private readonly EncounterConfig _config;

        public EncounterTriggerService(IEncounterRng rng, EncounterConfig config)
        {
            _rng = rng;
            _config = config;
        }

        public bool TryTriggerEncounter(IWorldGridMap map, Vector2Int playerPosition)
        {
            if (map.GetTileType(playerPosition) != WorldTileType.Biome)
            {
                return false;
            }

            return _rng.Next01() <= _config.BiomeEncounterChance;
        }
    }
}
