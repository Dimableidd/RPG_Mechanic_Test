using Game.Types;
using Logics.Health;
using Logics.Movement;
using UnityEngine;

namespace Game.Entities
{
    public class Actor : MonoBehaviour
    {
        [field: SerializeField] public Transform SelfTansform { get; private set; }
        [field: SerializeField] public NavMeshMovement Movement { get; private set; }
        [field: SerializeField] public Animator animator { get; private set; }
        [field: SerializeField] public CHealth Health { get; private set; }
        [field: SerializeField] public ETeam Team { get; private set; }

        public Vector3 GetLocation() => SelfTansform != null ? SelfTansform.position : transform.position;
        public void SetLocation(Vector3 Pos){
            transform.position = Pos;
        }

        public virtual void ApplyDamage(float amount)
        {
            if (Health != null && Health.IsAlive)
            {
                Health.SubtractHealth(amount);
            }
            else
            {
                OnDied();
            }
        }

        protected virtual void OnDied()
        {
            Destroy(gameObject);
        }
    }
}
