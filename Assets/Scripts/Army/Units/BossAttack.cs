using Army.PlayerArmy;
using System.Collections;
using UnityEngine;

namespace Army.Units
{
    public class BossAttack : Unit
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private LayerMask _attackableObjects;

        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;

        private bool _isAttacking = false;

        private PlayerArmyMover _playerArmy;

        private void Awake()
        {
            _playerArmy = FindObjectOfType<PlayerArmyMover>();
        }

        private void Update()
        {
            if (_isAttacking)
                transform.LookAt(_playerArmy.transform);
        }

        public override void Fire(int damage, LayerMask attackableObjects)
        {
            var newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            newBullet.SetInformation(damage, attackableObjects, UnitType);
        }

        public IEnumerator Attack()
        {
            _isAttacking = true;

            while (true)
            {
                Fire(_damage, _attackableObjects);
                yield return new WaitForSeconds(_fireRate);
            }
        }
    }
}