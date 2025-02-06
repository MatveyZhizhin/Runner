using System.Collections;
using Army.PlayerArmy;
using Army.Units;
using HealthOfObjects;
using UnityEngine;

namespace Army
{
    [RequireComponent(typeof(ArmyManager))]
    public class ArmyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _minimumAttackDistance;
        [SerializeField] private float _attackRate;

        [SerializeField] private LayerMask _attackableObjects;

        [SerializeField] private Transform _attackPoint;

        private ArmyManager _armyManager;
        private PlayerArmyMover _playerArmyMover;

        private bool _isAttacking;

        private void Awake()
        {
            TryGetComponent(out _armyManager);
            _playerArmyMover = FindObjectOfType<PlayerArmyMover>();
        }

        private void Update()
        {
            FindAttackableObjects();
        }

        private void FindAttackableObjects()
        {

            Ray ray = new Ray(_attackPoint.position, Vector3.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _attackDistance, _attackableObjects))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.TryGetComponent(out Health health))
                    {
                        if (!_isAttacking)
                        {
                            StartCoroutine(Attack());
                            _isAttacking = true;
                        }                      
                    }
                    else
                    {
                        StopAllCoroutines();
                        _isAttacking = false;
                    }
                }
            }

            if (Physics.Raycast(ray, out hitInfo, _minimumAttackDistance, _attackableObjects))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.TryGetComponent(out ArmyManager armyManager))
                    {
                        if(hitInfo.collider.GetComponentInChildren<Unit>().UnitType == UnitTypes.Enemy)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private IEnumerator Attack()
        {
            var units = _armyManager.GetUnits();

            while (true)
            {
                foreach (var unit in units)
                {
                    unit.Fire();
                }

                yield return new WaitForSeconds(_attackRate);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_attackPoint.position, Vector3.forward * _attackDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_attackPoint.position, Vector3.forward * _minimumAttackDistance);
        }
    }

}
