using Army.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Army
{
    public class ArmyManager : MonoBehaviour
    {
        [SerializeField] private Unit _unitPrefab;
        private List<Unit> _spawnedUnits = new List<Unit>();

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _maximumAmountOfUnitsInRow;
        [SerializeField] private float _unitScaleMultiplier = 1;

        private Vector3 _unitSize;

        private void Awake()
        {
            _unitSize = _unitPrefab.transform.localScale * _unitScaleMultiplier;
        }

        private void Start()
        {
            if (_unitPrefab.UnitType != UnitTypes.Player)
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _unitSize.x, _spawnPoint.localPosition.z + _unitSize.z * (_maximumAmountOfUnitsInRow / 2));
        }

        public Unit[] GetUnits()
        {
            return _spawnedUnits.ToArray();
        }

        public void AddUnit(int amount = 1)
        {
            if (_spawnedUnits.Count == 0 && _unitPrefab.UnitType == UnitTypes.Player)
            {
                var newUnit = Instantiate(_unitPrefab, _spawnPoint.position, _spawnPoint.rotation);
                newUnit.transform.parent = transform;
                _spawnedUnits.Add(newUnit);
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _unitSize.x, _spawnPoint.localPosition.z + _unitSize.z * (_maximumAmountOfUnitsInRow / 2));
                amount -= 1;
            }

            for (int i = 0; i < amount; i++)
            {                           
                var newUnit = Instantiate(_unitPrefab, _spawnPoint.position, _spawnPoint.rotation);
                newUnit.transform.parent = transform;
                _spawnedUnits.Add(newUnit);
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x, _spawnPoint.localPosition.z - _unitSize.z);


                if (CalculateCurrentAmountOfUnitsInLastRow() >= _maximumAmountOfUnitsInRow)
                {
                    ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _unitSize.x, _spawnPoint.localPosition.z + _unitSize.z * _maximumAmountOfUnitsInRow);
                }
            }
        }

        public void RemoveUnit(int amount = 1)
        {

            if (amount >= _spawnedUnits.Count)
            {
                if (_unitPrefab.UnitType == UnitTypes.Player)
                    amount = _spawnedUnits.Count - 1;
                else
                    amount = _spawnedUnits.Count;
            }  
            
            var startSpawnedUnitsCount = _spawnedUnits.Count;

            for (int i = _spawnedUnits.Count; i > startSpawnedUnitsCount - amount; i--)
            {
                Destroy(_spawnedUnits[i - 1].gameObject);
                ChangeSpawnPointPosition(_spawnedUnits[i - 1].transform.localPosition.x, _spawnedUnits[i - 1].transform.localPosition.z);
                _spawnedUnits.RemoveAt(i - 1);
            }
        }


        private void ChangeSpawnPointPosition(float newPositionX = 0, float newPositionZ = 0)
        {
            _spawnPoint.localPosition = new Vector3(newPositionX, _spawnPoint.localPosition.y, newPositionZ);
        }


        private int CalculateCurrentAmountOfUnitsInLastRow()
        {
            if(_unitPrefab.UnitType == UnitTypes.Player)
            {
                if ((_spawnedUnits.Count - 1) % _maximumAmountOfUnitsInRow != 0)
                    return _spawnedUnits.Count % _maximumAmountOfUnitsInRow;
                else
                    return _maximumAmountOfUnitsInRow;
            }

            if ((_spawnedUnits.Count) % _maximumAmountOfUnitsInRow != 0)
                return _spawnedUnits.Count % _maximumAmountOfUnitsInRow;

            return _maximumAmountOfUnitsInRow;
        }
    }
}
