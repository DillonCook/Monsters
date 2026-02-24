using System;

namespace Monsters.World.Encounter
{
    public sealed class SeededEncounterRng : IEncounterRng
    {
        private readonly Random _random;

        public SeededEncounterRng(int seed)
        {
            _random = new Random(seed);
        }

        public float Next01()
        {
            return (float)_random.NextDouble();
        }
    }
}
