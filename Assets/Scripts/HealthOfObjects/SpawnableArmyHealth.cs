using UnityEngine;

namespace HealthOfObjects
{
    public class SpawnableArmyHealth : ArmyHealth
    {
        [SerializeField] private int _minimumStartHealth;
        [SerializeField] private int _maximumStartHealth;

        private void Start()
        {
            _startHealth = Random.Range(_minimumStartHealth, _maximumStartHealth);
            AddHealth(_startHealth);
        }
    }
}
