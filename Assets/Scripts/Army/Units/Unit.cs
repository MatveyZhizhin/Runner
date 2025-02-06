using UnityEngine;

namespace Army.Units
{
    public class Unit : MonoBehaviour
    {
        public UnitTypes UnitType;

        public void Fire()
        {
            Debug.Log("Fire");
        }
    }
}
