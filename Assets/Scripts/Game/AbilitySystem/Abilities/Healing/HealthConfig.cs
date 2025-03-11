using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Healing
{
    [CreateAssetMenu(menuName = "Game/Abilities/Health", fileName = "HealthConfig")]
    public class HealthConfig : AbilityConfig
    {
        [field: SerializeField] public float HealthCount { get; private set; }
        [field: SerializeField] public GameObject Health { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new HealthBuilder(this);
        }
    }
}
