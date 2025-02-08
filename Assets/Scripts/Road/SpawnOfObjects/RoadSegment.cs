using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Road.SpawnOfObjects
{
    public class RoadSegment : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private SpawnableObject[] _spawnableObjects;

        private SpawnManager _spawnManager;

        [SerializeField] private bool _isLastSegment;

        private void Awake()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var objectTypes = Enum.GetValues(typeof(SpawnableObjects));
            var spawnedObjects = new List<SpawnableObject>();
            foreach (var spawnPoint in _spawnPoints)
            {
                var newObjectType = (SpawnableObjects)objectTypes.GetValue(Random.Range(0, objectTypes.Length));

                if (!_spawnManager.HasSpace())
                    newObjectType = SpawnableObjects.Nothing;

                if(_isLastSegment)
                    newObjectType = SpawnableObjects.Boss;

                foreach (var spawnableObject in _spawnableObjects)
                {               
                    if (spawnableObject.ObjectType == newObjectType)
                    {
                      var newObject = Instantiate(spawnableObject, spawnPoint.position, spawnableObject.transform.rotation);
                      newObject.transform.parent = transform;
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
    }
}
