using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Bullet
{
    public class BulletBuilder : AbilityBuilder
    {
        private readonly BulletConfig _config;

        public BulletBuilder(BulletConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new BulletAbility(_config.Damage, _config.Bullet,_config.Flash, _config.Hit);

            base.Make();
        }
    }
}