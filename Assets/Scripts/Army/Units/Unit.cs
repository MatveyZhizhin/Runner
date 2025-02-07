using UnityEngine;

namespace Army.Units
{
    public class Unit : MonoBehaviour
    {
        public UnitTypes UnitType;

        public virtual void Fire(int damage, LayerMask attackableObjects) { }
    }
}
