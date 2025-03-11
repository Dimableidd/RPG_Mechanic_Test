using System;
using System.Collections;
using Logics.Health;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.EnemyUI
{
    public class UIHealth_Bar : MonoBehaviour
    {
        public event Action<int> OnClick;
        [SerializeField] public Image _Bar;
        [SerializeField] public Text _Text;
        private Actor _actor;
        private CHealth Health;

        public void Show(Actor actor)
        {
            _actor = actor;
        }

        public void Clear()
        {
            _actor = null;
        }

        private void Start() 
        {
            gameObject.SetActive(false);
        }

        private void Update() 
        {
            if (_actor != null) 
            {
                _Bar.fillAmount = _actor.GetComponent<CHealth>().CurrentHealth / _actor.GetComponent<CHealth>().MaxHealth;
                _Text.text = _actor.gameObject.name;
            }
            else gameObject.SetActive(false);
        }
    }
}