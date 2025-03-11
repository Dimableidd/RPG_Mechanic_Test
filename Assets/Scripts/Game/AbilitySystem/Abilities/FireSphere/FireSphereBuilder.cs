using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireSphere
{
    public class FireSphereBuilder : AbilityBuilder
    {
        private readonly FireSphereConfig _config;

        public FireSphereBuilder(FireSphereConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new FireSphereAbility(_config.Damage, _config.Duration, _config.FireSphere);

            base.Make();
        }
    }
}