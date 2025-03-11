using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Explosions
{
    [CreateAssetMenu(menuName = "Game/Abilities/Explosions", fileName = "ExplosionsConfig")]

    public class ExplosionsConfig : AbilityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public GameObject Explosion { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new ExplosionsBuilder(this);
        }
    }
}