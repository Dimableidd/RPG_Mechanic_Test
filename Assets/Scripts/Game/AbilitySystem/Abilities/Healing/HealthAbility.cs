using Game.Entities;
using Logics.Mana;
using Logics.Health;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Healing
{
    public class HealthAbility : Ability
    {
        public float HealthCount { get; private set; }
        public GameObject Health { get; private set; }

        private Actor _target, _actorOwner;

        private Vector3 _Des = Vector3.zero;

        public HealthAbility(float healthCount, GameObject health)
        {
            HealthCount = healthCount;
            Health = health;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner == null || target == null) return false;
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if(owner.Team == target.Team)
                {
                    _Des = new Vector3 (owner.SelfTansform.position.x, owner.SelfTansform.position.y + 3f, owner.SelfTansform.position.z);
                    _target = target;
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                }
            }

            return false;
        }

        public override void ApplyCast()
        {
            if(_target != null) 
            {
                var temp = GameObject.Instantiate(Health, _Des, Quaternion.identity).GetComponent<Blink_health>();
                temp.Init(_Des);
                _actorOwner.animator.SetTrigger("SPlace");
                _target.GetComponent<CHealth>().AddHealth(HealthCount);
                
                ChangeCooldownTimer(CooldownTime);
                ChangeStatus(EAbilityStatus.Cooldown);
            }
        }

        public override void EventTick(float deltaTick)
        {
            if(Status == EAbilityStatus.Cooldown)
            {
                ChangeCooldownTimer(CooldownTimer - deltaTick);

                if (CooldownTimer <= 0.0f)
                    ChangeStatus(EAbilityStatus.Ready);
            }
        }

    }
}
