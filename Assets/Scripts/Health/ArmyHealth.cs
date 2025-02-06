using Army;
using UnityEngine;

namespace Health
{
    public class ArmyHealth : Health
    {
        [SerializeField] private int _healthPerUnit;

        private ArmyManager _armyManager;

        private void Awake()
        {
            TryGetComponent(out _armyManager);
        }

        public override void AddHealth(int additionalHealth)
        {
            base.AddHealth(additionalHealth);

            var difference = (int)_currentHealth / _healthPerUnit - _armyManager.GetUnitsCount();

            if (_currentHealth > _armyManager.GetUnitsCount() * _healthPerUnit)
            {
                if (_currentHealth % _healthPerUnit != 0)
                {
                    _armyManager.AddUnit(difference + 1);
                    return;
                }

                _armyManager.AddUnit(difference);
            }             
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            var difference = _armyManager.GetUnitsCount() - (int)_currentHealth / _healthPerUnit;

            if (_currentHealth < _armyManager.GetUnitsCount() * _healthPerUnit)
            {
                if (_currentHealth % _healthPerUnit != 0)
                {
                    _armyManager.RemoveUnit(difference - 1);
                    return;
                }
            }
                _armyManager.RemoveUnit(difference);
        }
    }
}

