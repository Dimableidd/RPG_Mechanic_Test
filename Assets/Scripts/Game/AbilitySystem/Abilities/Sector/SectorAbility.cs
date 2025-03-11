using Game.Entities;
using Logics.Mana;
using Game.AbilitySystem.Abilities.Sector;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.Sector
{
    public class SectorAbility : Ability
    {
        public float Damage { get; private set; }
        public GameObject SectorObj { get; private set; }

        private Vector3 _Des = Vector3.zero;
        private Vector3 _Pos = Vector3.zero;
        private Actor _actorOwner;

        public SectorAbility (float damage, GameObject sector)
        {
            Damage = damage;
            SectorObj = sector;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if (location != Vector3.zero){
                    _Des = new Vector3(location.x, location.y + 1f, location.z);
                    _Pos = new Vector3 (owner.SelfTansform.position.x, 1f, owner.SelfTansform.position.z);
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                    }
                else if (target != null){
                    _Des = new Vector3(target.SelfTansform.position.x, target.SelfTansform.position.y + 1f, target.SelfTansform.position.z);
                    _Pos = new Vector3 (owner.SelfTansform.position.x, 1f, owner.SelfTansform.position.z);
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                }
            }

            return false;
        }

        public override void ApplyCast()
        {            
            _actorOwner.animator.SetTrigger("STarget");
            Vector3 Difference = new Vector3(_Des.x - _Pos.x, 0f, _Des.z - _Pos.z);
            _actorOwner.SelfTansform.rotation = Quaternion.LookRotation(Difference);
            var temp = GameObject.Instantiate(SectorObj, _Pos, Quaternion.identity).GetComponent<SectorAb>();
            temp.Init(Damage, _Pos, _Des);

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