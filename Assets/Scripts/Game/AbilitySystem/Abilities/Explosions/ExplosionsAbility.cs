using Game.Entities;
using Logics.Mana;
using Game.Types;
using Logics.Health;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Explosions
{
    public class ExplosionsAbility : Ability
    {
        public float Damage { get; private set; }
        public GameObject Explosion { get; private set; }

        private Actor _actorOwner, _target;
        private Vector3 _Pos = Vector3.zero;

        public ExplosionsAbility(float damage, GameObject explosion)
        {
            Damage = damage;
            Explosion = explosion;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner == null || target == null) return false;
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if(target.Team == ETeam.Team_2)
                {
                    _Pos = new Vector3 (target.SelfTansform.position.x, target.SelfTansform.position.y + 1f, target.SelfTansform.position.z);
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
                _actorOwner.animator.SetTrigger("STarget");
                _actorOwner.SelfTansform.LookAt(_target.SelfTansform);
                var temp = GameObject.Instantiate(Explosion, _Pos, Quaternion.identity).GetComponent<ExplosionsAB>();
                temp.Init(Damage, _target);
                
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