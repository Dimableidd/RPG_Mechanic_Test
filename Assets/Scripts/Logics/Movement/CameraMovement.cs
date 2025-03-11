using UnityEngine;

namespace Logics.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smooth;
        [SerializeField] private Transform _target;

        private Transform _selfTransform;

        private void Awake()
        {
            _selfTransform = GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            if (_target != null)
            {
                Vector3 deltaMove = _target.position;
                deltaMove.y = _selfTransform.position.y;

                _selfTransform.position = Vector3.Lerp(_selfTransform.position + _offset, deltaMove, _smooth);

            }
        }
    }
}
