using HealthOfObjects;
using Road.SpawnOfObjects;
using UnityEngine;

namespace Army.Units
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;
        [SerializeField] private float _speed;

        private LayerMask _attackableObjects;
        private UnitTypes _unitType;

        [SerializeField] private float _distance;
        [SerializeField] private float _lifeTime; 

        public void SetInformation(int damage, LayerMask attackableObjects, UnitTypes unitType)
        {
            _damage = damage;
            _attackableObjects = attackableObjects;
            _unitType = unitType;
        }

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _distance, _attackableObjects))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.TryGetComponent(out Health health))
                    {
                        if (_unitType == UnitTypes.Player)
                        {
                            if (hitInfo.collider.TryGetComponent(out SpawnableObject spawnableObject))
                            {
                                health.TakeDamage(_damage);
                                Destroy(gameObject);
                            }                                                        
                        }
                        else
                        {
                            if (hitInfo.collider.TryGetComponent(out ArmyManager armyManager))
                            {
                                if (armyManager.GetComponentInChildren<Unit>().UnitType == UnitTypes.Player)
                                {
                                    health.TakeDamage(_damage);
                                    Destroy(gameObject);
                                }                                  
                            }
                        }
                    }
                }              
            }
            Destroy(gameObject, _lifeTime);

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * _distance);
        }
    }
}
