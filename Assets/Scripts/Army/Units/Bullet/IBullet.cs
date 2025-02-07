using UnityEngine;

namespace Army.Units.Bullet
{
    public interface IBullet
    {
        public void SetInformation(int damage, LayerMask attackableObjects, UnitTypes unitType);
    }
}
