using Services.InputHandler;
using Services.Locator;
using UnityEngine;
using UnityEngine.AI;
using Game.AbilitySystem;

namespace Logics.Movement
{
    public class NavMeshMovement : MonoBehaviour, IInjectServices
    {
        [field: SerializeField] public float Speed { get; private set; }
        [SerializeField] private ParticleSystem clickEffect;

        [SerializeField] private LayerMask _movementLayers;

        private NavMeshAgent _agent;
        private Camera _camera;
        private PlayerInput _playerInput;
        private Animator _animator;
        private bool _status;
        private float lookRotationSpeed = 8f, _mouseDownTime, holdThreshold = 0.2f;

        public void Inject(IServiceLocator locator)
        {
            _playerInput = locator.GetService<PlayerInput>();
        }

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            if (_agent == null)
            {
                _agent = gameObject.AddComponent<NavMeshAgent>();
            }

            _agent.speed = Speed;
        }

        public void Status(bool Status){
            _status = Status;
        }

        private void Update()
        {
            if (_playerInput != null && !_status)
            {
                if (_playerInput.OnLeftMouseDown())
                {
                    _mouseDownTime = Time.time;
                }

                if (_playerInput.OnLeftMouseUp())
                {
                    float mouseHoldDuration = Time.time - _mouseDownTime;

                    if (mouseHoldDuration <= holdThreshold)
                    {
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out var hitResult, 150.0f, _movementLayers) && !hitResult.collider.CompareTag("Enemy"))
                        {
                            _animator.SetBool("Move", true);
                            _agent.SetDestination(hitResult.point);
                            if (clickEffect != null)
                            {
                                Instantiate(clickEffect, hitResult.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                            }
                        }
                    }
                }
                else if (_playerInput.OnLeftMouseGet())
                    {
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out var hitResult, 150.0f, _movementLayers) && !hitResult.collider.CompareTag("Enemy"))
                        {
                            _animator.SetBool("Move", true);
                            _agent.SetDestination(hitResult.point);
                        }
                    }
            }

            _animator.SetFloat("speed", _agent.velocity.magnitude / _agent.speed);
            FaceTarget();
        }

        void FaceTarget()
        {
            if (_agent.destination == transform.position) return;
            Vector3 direction = (_agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }

        public void StopMovement()
        {
            _agent.ResetPath();
        }

        public void EnableMovement()
        {
            _agent.enabled = true;
            _agent.isStopped = false;
        }

        public void DisableMovement()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }
}
