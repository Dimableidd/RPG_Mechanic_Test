using Game.Entities;
using Logics.Mana;
using Game.AbilitySystem.Abilities.FireSphere;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireSphere
{
    public class FireSphereAbility : Ability
    {
        public float Damage { get; private set; }
        public float Duration { get; private set; }
        public GameObject FireSphere { get; private set; }

        private Vector3 _Des = Vector3.zero;
        private Vector3 _Pos = Vector3.zero;
        private Actor _actorOwner;

        public FireSphereAbility (float damage, float duration, GameObject fireSphere)
        {
            Damage = damage;
            Duration = duration;
            FireSphere = fireSphere;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {   
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if (location != Vector3.zero)
                {
                    _Des = new Vector3(location.x, 1f, location.z);
                    _Pos = new Vector3 (owner.SelfTansform.position.x, owner.SelfTansform.position.y + 1f, owner.SelfTansform.position.z);
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                }
                else if (target != null)
                {
                    _Des = new Vector3(target.SelfTansform.position.x, target.SelfTansform.position.y + 1f, target.SelfTansform.position.z);
                    _Pos = new Vector3 (owner.SelfTansform.position.x, owner.SelfTansform.position.y + 1f, owner.SelfTansform.position.z);
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                }
            }

            return false;
        }

        public override void ApplyCast()
        {            
            Vector3 Difference = new Vector3(_Des.x - _Pos.x, 0f, _Des.z - _Pos.z);
            _actorOwner.SelfTansform.rotation = Quaternion.LookRotation(Difference);
            _actorOwner.animator.SetTrigger("STarget");
            var temp = GameObject.Instantiate(FireSphere, _Pos, Quaternion.identity).GetComponent<FireBallSphere>();
            temp.Init(Damage, Duration, _Pos, _Des);

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