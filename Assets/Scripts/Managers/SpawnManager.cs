using System.Collections.Generic;
using Road;
using Road.SpawnOfObjects;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private List<SpawnableObject> _spawnedObjects = new List<SpawnableObject>();
        [SerializeField] private int _maxAmountOfObjects;

        private List<SpawnableObjects> _spawnableTypes = new List<SpawnableObjects>();

        private RoadGenerator _roadGenerator;
        private LevelManager _levelManager;

        private void Awake()
        {
            _roadGenerator = FindObjectOfType<RoadGenerator>();
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            Spawn(_levelManager.IsLevelRestarted);
        }

        private void Spawn(bool isLevelRestarted)
        {
            foreach (var spawnedSegment in _roadGenerator.GetRoadSegments())
            {
                if (isLevelRestarted)
                {
                    spawnedSegment.Spawn(GetSpawnableTypes(spawnedSegment.IsLastSegment));
                }
                else
                {
                    spawnedSegment.Spawn();
                }
            }
        }

        public void SetSpawnableTypes(SpawnableObjects[] types)
        {
            foreach (var type in types)
            {
                _spawnableTypes.Add(type);
            }
        }

        public SpawnableObjects[] GetTypesOfSpawnedObjects()
        {
            var types = new List<SpawnableObjects>();

            foreach (var spawnedObject in _spawnedObjects)
            {
                types.Add(spawnedObject.ObjectType);
            }

            return types.ToArray();
        }

        private SpawnableObjects[] GetSpawnableTypes(bool isLastSegment)
        {
            var types = new List<SpawnableObjects>();

            for (int i = 0; i < _roadGenerator.GetSpawnPointsCount(isLastSegment); i++)
            {
                types.Add(_spawnableTypes[i]);
                _spawnableTypes.RemoveAt(i);
            }

            return types.ToArray();
        }

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
