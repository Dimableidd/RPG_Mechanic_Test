using Game.Entities;
using Logics.Mana;
using Game.AbilitySystem.Abilities.Blink;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Blink
{
    public class BlinkAbility : Ability
    {
        public GameObject Blink { get; private set; }

        private Vector3 _Des = Vector3.zero;
        private Vector3 _Pos = Vector3.zero;

        private Actor _actorOwner;
        public BlinkAbility (GameObject blink)
        {
            Blink = blink;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if (location != Vector3.zero)
                {
                    _Des = new Vector3(location.x, 0.1f, location.z);
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
            _actorOwner.animator.SetTrigger("SPlace");
            Vector3 Difference = new Vector3(_Des.x - _Pos.x, 0f, _Des.z - _Pos.z);
            _actorOwner.SelfTansform.rotation = Quaternion.LookRotation(Difference);
            var temp = GameObject.Instantiate(Blink, _Des, Quaternion.identity).GetComponent<Blinks>();;
            temp.Init(_actorOwner, _Des);

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