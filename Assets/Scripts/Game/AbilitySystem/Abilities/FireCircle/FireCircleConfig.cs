using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireCircle
{
    [CreateAssetMenu(menuName = "Game/Abilities/FireCircle", fileName = "FireCircleConfig")]
    public class FireCircleConfig : AbilityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Interval { get; private set; }
        [field: SerializeField] public int CountPerWave { get; private set; }
        [field: SerializeField] public GameObject epicentrPrefab { get; private set; }


        public override AbilityBuilder GetBuilder()
        {
            return new FireCircleBuilder(this);
        }
    }
}