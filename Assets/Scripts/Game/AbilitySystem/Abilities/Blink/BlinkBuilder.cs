using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Blink
{
    public class BlinkBuilder : AbilityBuilder
    {
        private readonly BlinkConfig _config;

        public BlinkBuilder(BlinkConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new BlinkAbility(_config.Blink);

            base.Make();
        }
    }
}