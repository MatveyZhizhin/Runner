using AnimatorsConstans;
using Army.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Army
{
    public class ArmyManager : MonoBehaviour
    {
        [SerializeField] private Unit _unitPrefab;
        protected List<Unit> _spawnedUnits = new List<Unit>();

        [SerializeField] protected Transform _spawnPoint;
        [SerializeField] protected int _maximumAmountOfUnitsInRow;
        [SerializeField] protected float _gapBetweenUnits = 1;

        [SerializeField] private float _unitDestroyTime;

        public int AmountOfUnits => _spawnedUnits.Count;

        private void Start()
        {
            if (_unitPrefab.UnitType != UnitTypes.Player)
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _gapBetweenUnits, _spawnPoint.localPosition.z + _gapBetweenUnits * (_maximumAmountOfUnitsInRow / 2));
        }

        public Unit[] GetUnits()
        {
            return _spawnedUnits.ToArray();
        }

        public virtual void AddUnit(int amount = 1)
        {           
            for (int i = 0; i < amount; i++)
            {                           
                var newUnit = Instantiate(_unitPrefab, _spawnPoint.position, _spawnPoint.rotation);
                newUnit.transform.parent = transform;
                _spawnedUnits.Add(newUnit);
                ChangeSpawnPointPosition(_spawnPoint.localPosition.x, _spawnPoint.localPosition.z - _gapBetweenUnits);


                if (CalculateCurrentAmountOfUnitsInLastRow() >= _maximumAmountOfUnitsInRow)
                {
                    ChangeSpawnPointPosition(_spawnPoint.localPosition.x - _gapBetweenUnits, _spawnPoint.localPosition.z + _gapBetweenUnits * _maximumAmountOfUnitsInRow);
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
                ChangeSpawnPointPosition(_spawnedUnits[i - 1].transform.localPosition.x, _spawnedUnits[i - 1].transform.localPosition.z);
                if (_unitPrefab.UnitType != UnitTypes.Neutral)
                {
                    _spawnedUnits[i - 1].GetComponent<Animator>().SetTrigger(CharacterAnimationConstans.Dead);
                    _spawnedUnits[i - 1].GetComponent<Animator>().SetBool(CharacterAnimationConstans.IsRunning, false);
                    _spawnedUnits[i - 1].transform.parent = null;
                }                 
                Destroy(_spawnedUnits[i - 1].gameObject, _unitDestroyTime);
                _spawnedUnits.RemoveAt(i - 1);               
            }
        }


        protected void ChangeSpawnPointPosition(float newPositionX = 0, float newPositionZ = 0)
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
