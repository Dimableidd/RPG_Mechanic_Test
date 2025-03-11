using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Sector
{
    public class SectorBuilder : AbilityBuilder
    {
        private readonly SectorConfig _config;

        public SectorBuilder(SectorConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new SectorAbility(_config.Damage, _config.SectorObj);

            base.Make();
        }
    }
}