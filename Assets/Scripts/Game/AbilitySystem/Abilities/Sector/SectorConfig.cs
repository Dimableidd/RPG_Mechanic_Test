using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Sector
{
    [CreateAssetMenu(menuName = "Game/Abilities/Sector", fileName = "SectorConfig")]
    public class SectorConfig : AbilityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public GameObject SectorObj { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new SectorBuilder(this);
        }
    }
}