using System;
using System.Collections;
using Logics.Mana;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.AbilitySystem.BarUI
{
    public class ManaBar : MonoBehaviour
    {
        public event Action<int> OnClick;
        [SerializeField] private Image _Bar;
        [SerializeField] private Actor _actor;
        private CMana Mana;

        private void Update() 
        {
            _Bar.fillAmount = _actor.GetComponent<CMana>().CurrentMana / _actor.GetComponent<CMana>().MaxMana;
        }
    }
}