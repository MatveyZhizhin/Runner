using Road;
using Road.SpawnOfObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        private SpawnManager _spawnManager;
        private RoadGenerator _roadGenerator;

        private void Awake()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
            _roadGenerator = FindObjectOfType<RoadGenerator>();
            _levelManager = FindObjectOfType<LevelManager>();
            Load();
        }

        public void Save()
        {
            var spawnedTypes = _spawnManager.GetTypesOfSpawnedObjects();

            for (int i = 0; i < spawnedTypes.Length; i++)
            {
                YandexGame.savesData.SpawnableTypes[i] = (int)spawnedTypes[i];
            }

            YandexGame.savesData.IsLevelRestarted = _levelManager.IsLevelRestarted;

            YandexGame.SaveProgress();
        }

        private void Load()
        {
            var spawnableTypes = new SpawnableObjects[(_roadGenerator.RoadSegmentCount - 1) * _roadGenerator.GetSpawnPointsCount(false) + _roadGenerator.GetSpawnPointsCount(true)];

            for (int i = 0; i < spawnableTypes.Length; i++)
            {
                spawnableTypes[i] = (SpawnableObjects)YandexGame.savesData.SpawnableTypes[i];
            }

            _spawnManager.SetSpawnableTypes(spawnableTypes);

            _levelManager.IsLevelRestarted = YandexGame.savesData.IsLevelRestarted;
        }
    }
}
