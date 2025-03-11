using UnityEngine;

namespace Logics.Mana
{
    public class CMana : MonoBehaviour
    {
        [field: SerializeField] public float MaxMana { get; private set; }
        [field: SerializeField] public float CurrentMana { get; private set; }
        [field: SerializeField] public float RegenerationMana { get; private set; }

        public void AddMana(float amount) => CurrentMana += amount;

        public void SubtractMana(float mana)
        {
            CurrentMana = Mathf.Max(CurrentMana - mana, 0.0f);
        }

        public void AddManaRegeneration(float amount) => RegenerationMana += amount;

        private void Update()
        {
            CurrentMana = Mathf.Min(CurrentMana + RegenerationMana * Time.deltaTime, MaxMana);
        }
    }
}