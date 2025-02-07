using UnityEngine;

namespace Army.Units.Bullets
{
    public interface IBullet
    {
        public void SetInformation(int damage, LayerMask attackableObjects, UnitTypes unitType);
    }
}
