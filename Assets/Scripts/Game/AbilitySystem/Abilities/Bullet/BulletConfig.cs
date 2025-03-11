using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Bullet
{
    [CreateAssetMenu(menuName = "Game/Abilities/Bullet", fileName = "BulletConfig")]
    public class BulletConfig : AbilityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public GameObject Bullet { get; private set; }
        [field: SerializeField] public GameObject Flash { get; private set; }
        [field: SerializeField] public GameObject Hit { get; private set; }
        
        

        public override AbilityBuilder GetBuilder()
        {
            return new BulletBuilder(this);
        }
    }
}