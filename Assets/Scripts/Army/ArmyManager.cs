using Army.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Army
{
    public class ArmyManager : MonoBehaviour
    {
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private List<Unit> _spawnedUnits;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _maximumAmountOfUnitsInRow;
        [SerializeField] private float _unitScaleMultiplier = 1;

        private Vector3 _unitSize;

        private void Start()
        {
            _unitSize = _unitPrefab.transform.localScale * _unitScaleMultiplier;
            ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _unitSize.x, _spawnPoint.localPosition.z + _unitSize.z * (_maximumAmountOfUnitsInRow / 2));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                AddUnit(50);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                RemoveUnit(20);
        }

        public void AddUnit(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {                           
                var newUnit = Instantiate(_unitPrefab, _spawnPoint.position, _unitPrefab.transform.localRotation);
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

            if (amount > _spawnedUnits.Count)
                return;

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
            if (_spawnedUnits.Count % _maximumAmountOfUnitsInRow != 0)
                return _spawnedUnits.Count % _maximumAmountOfUnitsInRow;

            return _maximumAmountOfUnitsInRow;
        }
    }
}
