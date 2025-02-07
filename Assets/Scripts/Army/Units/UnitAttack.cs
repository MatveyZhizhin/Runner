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
            var newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity);
            newBullet.SetInformation(damage, attackableObjects, UnitType);
        }
    }
}
