using Game.Entities;
using Logics.Mana;
using Game.AbilitySystem.Abilities.FireCircle;
using UnityEngine;

namespace Game.AbilitySystem.Abilities.FireCircle
{
    public class FireCircleAbility : Ability
    {
        public float Damage { get; private set; }
        public float Radius { get; private set; }
        public float Duration { get; private set; }
        public float Interval { get; private set; }
        public int CountPerWave { get; private set; }
        public GameObject epicentrPrefab { get; private set; }

        private Actor _actorOwner;
        private Vector3 _Loc = Vector3.zero;
        private Vector3 _Des = Vector3.zero;
        private Vector3 _Pos = Vector3.zero;

        public FireCircleAbility (float damage, float radius, float duration, float interval, int countPerWave, GameObject EpPrefab)
        {
            Damage = damage;
            Radius = radius;
            Duration = duration;
            Interval = interval;
            CountPerWave = countPerWave;
            epicentrPrefab = EpPrefab;
        }

        public override bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            if(owner.GetComponent<CMana>().CurrentMana >= ManaCost)
            {
                if (location != Vector3.zero)
                {
                    _Loc = new Vector3(location.x, location.y + 0.5f, location.z);
                    _Des = new Vector3(location.x, location.y + 1f, location.z);
                    _Pos = new Vector3 (owner.SelfTansform.position.x, owner.SelfTansform.position.y + 1f, owner.SelfTansform.position.z);
                    _actorOwner = owner;
                    owner.GetComponent<CMana>().SubtractMana(ManaCost);
                    return true;
                }
                else if (target != null)
                {
                    _Loc = new Vector3(target.SelfTansform.position.x, target.SelfTansform.position.y + 0.5f, target.SelfTansform.position.z);
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
            _actorOwner.animator.SetTrigger("SPlace");
            Vector3 Difference = new Vector3(_Des.x - _Pos.x, 0f, _Des.z - _Pos.z);
            _actorOwner.SelfTansform.rotation = Quaternion.LookRotation(Difference);
            var temp = GameObject.Instantiate(epicentrPrefab, _Loc, Quaternion.identity).GetComponent<Epicentr>();
            temp.Init(Damage, Duration, Radius, Interval, CountPerWave, _Loc);

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
