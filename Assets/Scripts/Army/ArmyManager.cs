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

        public void AddUnit(int amount = 1)
        {
            
        }


        private void ChangeSpawnPointPosition(float newPostionX = 0, float newPositionZ = 0)
        {
            _spawnPoint.localPosition = new Vector3(newPostionX, _spawnPoint.localPosition.y, newPositionZ);
        }
    }
}
