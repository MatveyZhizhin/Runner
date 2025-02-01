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

                foreach (var spawnableObject in _spawnableObjects)
                {               
                    if (spawnableObject.ObjectType == newObjectType)
                    {
                      var newObject = Instantiate(spawnableObject, spawnPoint.position, spawnableObject.transform.rotation);
                      newObject.transform.parent = transform;
                      spawnedObjects.Add(newObject);

                      break;
                    }
                }
           } 

           foreach (var spawnedObject in spawnedObjects)
           {
                if (spawnedObject.ObjectType == SpawnableObjects.Nothing)
                {
                    _spawnManager.AddObjects(spawnedObjects);
                    return;
                }
           }     
           
           foreach (var spawnedObject in spawnedObjects)
           {
                Destroy(spawnedObject.gameObject);
           }

           Spawn();
        }
    }
}
