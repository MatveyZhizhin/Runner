using Army.Units;
using UnityEngine;

namespace Army.PlayerArmy
{
    public class PlayerArmyManager : ArmyManager
    {
        [SerializeField] private Unit _playerPrefab;

        public override void AddUnit(int amount = 1)
        {
            if (_spawnedUnits.Count == 0)
            {
                var newUnit = Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);
                newUnit.transform.parent = transform;
                _spawnedUnits.Add(newUnit);
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _gapBetweenUnits, _spawnPoint.localPosition.z + _gapBetweenUnits * (_maximumAmountOfUnitsInRow / 2));
                amount -= 1;
            }

            base.AddUnit(amount);
        }
    }
}
