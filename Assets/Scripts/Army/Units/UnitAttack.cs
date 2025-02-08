using Army.Units.Bullets;
using UnityEngine;

namespace Army.Units
{
    public class UnitAttack : Unit
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPoint;

        public override void Fire(int damage, LayerMask attackableObjects)
        {
            if (_spawnPoint != null)
            {
                var newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
                newBullet.SetInformation(damage, attackableObjects, UnitType);
            }          
        }
    }
}
