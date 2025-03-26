using Army;
using System.Linq;
using UnityEngine;
using YG;

namespace HealthOfObjects
{
    public class ArmyHealth : Health
    {
        [SerializeField] private int _healthPerUnit;

        private ArmyManager _armyManager;

        private int UnitsCount => _armyManager.GetUnits().Count();

        [SerializeField] private int _rewardId;

        private void Awake()
        {
            TryGetComponent(out _armyManager);
        }

        public override void AddHealth(int additionalHealth)
        {
            base.AddHealth(additionalHealth);

            var difference = (int)_currentHealth / _healthPerUnit - UnitsCount;

            if (_currentHealth > UnitsCount * _healthPerUnit)
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

            var difference = UnitsCount - (int)_currentHealth / _healthPerUnit;

            if (_currentHealth < UnitsCount * _healthPerUnit)
            {
                if (_currentHealth % _healthPerUnit != 0)
                {
                    _armyManager.RemoveUnit(difference - 1);
                    return;
                }
            }
                _armyManager.RemoveUnit(difference);
        }
        public void Heal(int percent)
        {
            if (_currentHealth < _startHealth)
            {

                AddHealth(_startHealth / 100 * percent);
            }
        }

        public void Revive(int id)
        {
            if (id != _rewardId)
                return;

            Time.timeScale = 1f;
            AddHealth(_startHealth);
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Revive;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Revive;
        }
    }
}

