using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireSphere
{
    [CreateAssetMenu(menuName = "Game/Abilities/FireSphere", fileName = "FireSphereConfig")]
    public class FireSphereConfig : AbilityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public GameObject FireSphere { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new FireSphereBuilder(this);
        }
    }
}