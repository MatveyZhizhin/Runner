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
        [SerializeField] private float _attackRate;

        [SerializeField] private LayerMask _attackableObjects;

        [SerializeField] private Transform _attackPoint;

        private ArmyManager _armyManager;

        private bool _isAttacking = false;

        private void Awake()
        {
            TryGetComponent(out _armyManager);
        }

        private void Update()
        {
            FindAttackableObjects();
        }

        private void FindAttackableObjects()
        {

            Ray ray = new Ray(_attackPoint.position, _attackPoint.forward);
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
                }               
            }
            else
            {
                StopAllCoroutines();
                _isAttacking = false;
            }
        }

        private IEnumerator Attack()
        {
            var units = _armyManager.GetUnits();

            while (true)
            {
                foreach (var unit in units)
                {
                    unit.Fire(_damage, _attackableObjects);
                }

                yield return new WaitForSeconds(_attackRate);
            }                                                                                       
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_attackPoint.position, _attackPoint.forward * _attackDistance);
        }
    }

}
