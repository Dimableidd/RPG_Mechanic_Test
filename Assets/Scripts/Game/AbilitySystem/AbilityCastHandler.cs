using Game.AbilitySystem.AbilityUI;
using Game.Entities;
using Services.InputHandler;
using Services.Locator;
using System.Collections;
using System.Collections.Generic;
using Logics.Movement;
using UnityEngine.AI;
using UnityEngine;

namespace Game.AbilitySystem
{

    public class AbilityCastHandler : MonoBehaviour, IInjectServices
    {
        [SerializeField] private AbilityStorage _abilityStorage;
        [SerializeField] private Actor _ownerActor;
        [SerializeField] private LayerMask _targetsLayer;
        [SerializeField] NavMeshMovement _movement;

        private List<Ability> _abilities = new();
        private Ability _currentAbility;
        private NavMeshAgent _agent;
        private Animator _animator;

        private Camera _camera;
        private PlayerInput _playerInput;
        private AbilityDisplaying _abilityDisplaying;
        private float mouseTimer = 0f, holdThreshold = 0.2f;
        private bool Status = false;

        public void Inject(IServiceLocator locator)
        {
            _camera = Camera.main;
            _playerInput = locator.GetService<PlayerInput>();
            _agent = _ownerActor.GetComponent<NavMeshAgent>();
            _abilityDisplaying = locator.GetService<AbilityDisplaying>();
            _animator = _ownerActor.GetComponent<Animator>();

            _abilityStorage.Init();
            _abilities.AddRange(_abilityStorage.GetAbilities());

            _abilityDisplaying.Init(_abilityStorage.GetAbilities());
            _abilityDisplaying.OnClickAbility += OnClickAbilityButton;
        }

        public void OnClickAbilityButton(int abilityIndex)
        {
            _currentAbility?.CancelCast();

            switch (_abilities[abilityIndex].Status)
            {
                case EAbilityStatus.Ready:

                    _currentAbility = _abilities[abilityIndex];
                    _currentAbility.StartCast();

                    break;
                case EAbilityStatus.Cooldown:
                    break;
                case EAbilityStatus.NeedMana:
                    break;
            }
        }

        private void Update()
        {

            if (_playerInput != null)
            {
                if (_playerInput.OnLeftMouseUp())
                {
                        if( mouseTimer < holdThreshold)
                        {
                            _ownerActor.GetComponent<Shot>().ShotTime(true, _animator);
                            _abilities[5].EventTick(Time.deltaTime);
                            OnClickAbilityButton(5);
                        }
                        else _animator.SetBool("Move", true);
                        mouseTimer = 0f;
                }
                else if (_playerInput.OnLeftMouseGet())
                    {
                        mouseTimer += Time.deltaTime;
                        if( mouseTimer > holdThreshold)
                        {
                            _abilities[5].EventTick(Time.deltaTime);
                            OnClickAbilityButton(5);
                        }
                    }
            }

            for (int i = 0; i < _abilities.Count; ++i)
            {
                _abilities[i].EventTick(Time.deltaTime);

                if (_playerInput.OnKeyDown(_abilities[i].HotKey))
                {
                    OnClickAbilityButton(i);
                }
            }

            if(_currentAbility != null) 
            {
                /*if(_playerInput.OnRightMouseDown())
                {
                    _currentAbility.CancelCast();
                    _currentAbility = null;
                    return;
                }*/

                Vector3 location = Vector3.zero;
                Actor target = null;

                Ray ray = _camera.ScreenPointToRay(_playerInput.MousePosition());
                RaycastHit hitResult;
                if(Physics.Raycast(ray, out hitResult, 500.0f, _targetsLayer))
                {
                    hitResult.collider.TryGetComponent<Actor>(out target);
                    
                    if (hitResult.collider.CompareTag("Ground"))
                    {
                        location = hitResult.point;
                    }

                    if(target != null || location != Vector3.zero){
                        if(_currentAbility.CheckCondition(_ownerActor, target, location))
                        {
                            _movement.Status(true);
                            //if(_playerInput.OnLeftMouseDown())
                            //{ 
                                _agent.isStopped = true;
                                _agent.ResetPath();
                                _currentAbility.ApplyCast();
                            //}
                        }
                    }
                }
                    _currentAbility.CancelCast();
                    _currentAbility = null;
            }
            else _movement.Status(false);
        }

        private void OnDestroy()
        {
            if(_abilityDisplaying != null)
                _abilityDisplaying.OnClickAbility -= OnClickAbilityButton;
        }
        
    }
}
