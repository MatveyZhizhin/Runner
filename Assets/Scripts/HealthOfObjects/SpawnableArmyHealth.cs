using Managers;
using UnityEngine;

namespace HealthOfObjects
{
    public class SpawnableArmyHealth : ArmyHealth
    {
        [SerializeField] private int _minimumStartHealth;
        [SerializeField] private int _maximumStartHealth;

        public int MinimumStartHealth { get => _minimumStartHealth; set => _minimumStartHealth = value; }
        public int MaximumStartHealth { get => _maximumStartHealth; set => _maximumStartHealth = value; }

        private void Start()
        {
            _startHealth = Random.Range(_minimumStartHealth, _maximumStartHealth);
            AddHealth(_startHealth);
            _spawnManager = FindObjectOfType<SpawnManager>();
        }
    }
}
