using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Blink
{
    [CreateAssetMenu(menuName = "Game/Abilities/Blink", fileName = "BlinkConfig")]
    public class BlinkConfig : AbilityConfig
    {
        [field: SerializeField] public GameObject Blink { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new BlinkBuilder(this);
        }
    }
}