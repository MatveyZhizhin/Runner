using System.Collections.Generic;
using UnityEngine;

namespace Road.SpawnOfObjects
{
    public class SpawnManager : MonoBehaviour
    {
        private List<SpawnableObject> _spawnedObjects;

        public void AddObjects(List<SpawnableObject> spawnedObjects)
        {
            _spawnedObjects = spawnedObjects;
        }

        public void RemoveObject(SpawnableObject spawnedObject)
        {
            _spawnedObjects.Remove(spawnedObject);
        }
    }
}
