using HealthOfObjects;
using Road.SpawnOfObjects;
using UnityEngine;

namespace Army.Units.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        private int _damage;
        private LayerMask _attackableObjects;
        private UnitTypes _unitType;

        [SerializeField] private float _distance;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        public void SetInformation(int damage, LayerMask attackableObjects, UnitTypes unitType)
        {
            _damage = damage;
            _attackableObjects = attackableObjects;
            _unitType = unitType;
        }

        private void Update()
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
                    if (TryGetComponent(out Health health))
                    {
                        if (_unitType == UnitTypes.Player)
                        {
                            if (TryGetComponent(out SpawnableObject spawnableObject))
                            {
                                health.TakeDamage(_damage);
                            }
                        }
                        else
                        {
                            if (TryGetComponent(out ArmyManager armyManager))
                            {
                                health.TakeDamage(_damage);
                            }
                        }
                    }
                    Destroy(gameObject);
                }              
            }
            Destroy(gameObject, _lifeTime);

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
