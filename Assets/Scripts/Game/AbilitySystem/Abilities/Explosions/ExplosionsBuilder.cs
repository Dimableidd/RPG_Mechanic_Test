using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Explosions
{
    public class ExplosionsBuilder : AbilityBuilder
    {
        private readonly ExplosionsConfig _config;

        public ExplosionsBuilder(ExplosionsConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new ExplosionsAbility(_config.Damage, _config.Explosion);

            base.Make();
        }
    }
}
