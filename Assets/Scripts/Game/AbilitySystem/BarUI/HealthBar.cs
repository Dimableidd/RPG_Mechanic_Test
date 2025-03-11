using System;
using System.Collections;
using Logics.Health;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.AbilitySystem.BarUI
{
    public class HealthBar : MonoBehaviour
    {
        public event Action<int> OnClick;
        [SerializeField] private Image _Bar;
        [SerializeField] private Actor _actor;
        private CHealth Health;

        private void Update() 
        {
            _Bar.fillAmount = _actor.GetComponent<CHealth>().CurrentHealth / _actor.GetComponent<CHealth>().MaxHealth;
        }
    }
}
