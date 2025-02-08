using System.Collections.Generic;
using UnityEngine;

namespace Road.SpawnOfObjects
{
    public class SpawnManager : MonoBehaviour
    {
        private List<SpawnableObject> _spawnedObjects = new List<SpawnableObject>();
        [SerializeField] private int _maxAmountOfObjects;

        public void AddObject(SpawnableObject spawnedObject)
        {
            _spawnedObjects.Add(spawnedObject);
        }

        public void RemoveObject(SpawnableObject spawnedObject)
        {
            _spawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject.gameObject);
        }

        public bool HasSpace()
        {
            var spawnedObstacles = new List<SpawnableObject>();

            foreach (var spawnedObject in _spawnedObjects)
            {
                if (spawnedObject.ObjectType != SpawnableObjects.Nothing)
                    spawnedObstacles.Add(spawnedObject);
            }

            if (spawnedObstacles.Count < _maxAmountOfObjects)
                return true;

            return false;
        }
    }
}
