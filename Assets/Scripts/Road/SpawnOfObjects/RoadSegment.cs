using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Random = UnityEngine.Random;

namespace Road.SpawnOfObjects
{
    public class RoadSegment : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private SpawnableObject[] _spawnableObjects;

        public int SpawnPointsCount => _spawnPoints.Length;

        private SpawnManager _spawnManager;

        [SerializeField] private bool _isLastSegment;

        public bool IsLastSegment => _isLastSegment;

        private void Awake()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
        }

        public void Spawn()
        {
            var spawnedObjects = new List<SpawnableObject>();
            foreach (var spawnPoint in _spawnPoints)
            {
                var newObjectType = GetRandomType();

                if (_isLastSegment)
                    newObjectType = SpawnableObjects.Boss;

                if (!_isLastSegment)
                {
                    if (!_spawnManager.HasSpace())
                        newObjectType = SpawnableObjects.Nothing;
                    if (!_isLastSegment)
                        while (newObjectType == SpawnableObjects.Boss)
                            newObjectType = GetRandomType();
                }                                              
                   
                foreach (var spawnableObject in _spawnableObjects)
                {                      
                    if (spawnableObject.ObjectType == newObjectType)
                    {
                      var newObject = Instantiate(spawnableObject, spawnPoint.position, spawnableObject.transform.rotation);
                      spawnedObjects.Add(newObject);
                      _spawnManager.AddObject(newObject);

                      break;
                    }
                }
            } 

            if (!_isLastSegment)
            {
                foreach (var spawnedObject in spawnedObjects)
                {
                    if (spawnedObject.ObjectType == SpawnableObjects.Nothing)
                    {
                        return;
                    }
                }

                foreach (var spawnedObject in spawnedObjects)
                {
                    _spawnManager.RemoveObject(spawnedObject);
                }

                Spawn();
            }         
        }

        public void Spawn(SpawnableObjects[] types)
        {
            var spawnedObjects = new List<SpawnableObject>();

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                foreach (var spawnableObject in _spawnableObjects)
                {
                    if (spawnableObject.ObjectType == types[i])
                    {
                        var newObject = Instantiate(spawnableObject, _spawnPoints[i].position, spawnableObject.transform.rotation);
                        spawnedObjects.Add(newObject);
                        _spawnManager.AddObject(newObject);

                        break;
                    }
                }
            }
        }

        private SpawnableObjects GetRandomType()
        {
            var objectTypes = Enum.GetValues(typeof(SpawnableObjects));

            return (SpawnableObjects)objectTypes.GetValue(Random.Range(0, objectTypes.Length));
        }
    }
}
