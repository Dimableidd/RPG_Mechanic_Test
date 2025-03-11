using Game.Entities;
using System;
using System.Collections;
using UnityEngine;

namespace Game.AbilitySystem
{
    public class Ability 
    {
        public event Action<float, float> EventChangeCooldownTimer;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite DisplayImage { get; private set; }

        public float CooldownTime { get; private set; }
        public float CooldownTimer { get; private set; }

        public float ManaCost { get; private set; }

        public KeyCode HotKey { get; private set; }

        public EAbilityStatus Status { get; private set; }
        

        public void SetDescription(string title, string description, Sprite displayImage)
        {
            Title = title;
            Description = description;
            DisplayImage = displayImage;
        }

        public void SetKey(KeyCode key) => HotKey = key;
        public float SetCooldownTime(float cooldown) => CooldownTime = cooldown;
        public void SetManaCost(float manaCost) => ManaCost = manaCost;
        public void ChangeStatus(EAbilityStatus status) => Status = status;
        public void ChangeCooldownTimer(float timer)
        {
            CooldownTimer = Mathf.Clamp(timer, 0.0f, CooldownTime);
            EventChangeCooldownTimer?.Invoke(CooldownTimer, CooldownTime);
        }

        public virtual void StartCast()
        {
        }

        public virtual bool CheckCondition(Actor owner, Actor target, Vector3 location = default)
        {
            return false;
        }

        public virtual void ApplyCast() { }
        public virtual void EventTick(float deltaTick) { }
        public virtual void CancelCast() { }
    }
}
