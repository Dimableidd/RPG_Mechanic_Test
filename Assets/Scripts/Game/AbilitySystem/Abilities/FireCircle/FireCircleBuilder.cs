using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireCircle
{
    public class FireCircleBuilder : AbilityBuilder
    {
        private readonly FireCircleConfig _config;

        public FireCircleBuilder(FireCircleConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new FireCircleAbility(_config.Damage, _config.Radius, _config.Duration, _config.Interval, _config.CountPerWave, _config.epicentrPrefab);

            base.Make();
        }
    }
}