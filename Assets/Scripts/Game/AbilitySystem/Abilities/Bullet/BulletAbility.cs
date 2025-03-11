using Game.Entities;
using Game.Types;
using Game.AbilitySystem.Abilities.Bullet;
using UnityEngine.AI;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Bullet
{
    public class BulletAbility : Ability
    {
        public float Damage { get; private set; }
        public float Duration { get; private set; }
        public GameObject Bullet { get; private set; }
        public GameObject Flash { get; private set; }
        public GameObject Hit { get; private set; }

        private Vector3 _Des = Vector3.zero;
        public GameObject Shot;
        private Actor _actorOwner, _targetOwner;

        public BulletAbility (float damage, GameObject bullet, GameObject flash, GameObject hit)
        {
            Damage = damage;
            Bullet = bullet;
            Flash = flash;
            Hit = hit;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner == null || target == null) return false;
            if(target.Team == ETeam.Team_2)
            {
                _Des = new Vector3(target.SelfTansform.position.x, target.SelfTansform.position.y + 1f, target.SelfTansform.position.z);
                _actorOwner = owner;
                _targetOwner = target;
                return true;
            }

            return false;
        }

        public override void ApplyCast()
        {     
            _actorOwner.SelfTansform.LookAt(_targetOwner.SelfTansform);
            _actorOwner.GetComponent<Shot>().Init(Damage, _Des, Flash, Hit, Bullet);
            _actorOwner.animator.SetBool("Move", false);
            _actorOwner.animator.SetTrigger("isAttacking");

            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(EAbilityStatus.Cooldown);

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