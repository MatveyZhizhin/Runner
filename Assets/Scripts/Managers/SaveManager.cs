using Balance;
using HealthOfObjects;
using Road;
using Road.SpawnOfObjects;
using SkinChangers;
using UnityEngine;
using YG;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        private SpawnManager _spawnManager;
        private RoadGenerator _roadGenerator;
        private BalanceCounter _balanceCounter;

        private BossSkinChanger _bossSkinChanger;
        private SpawnableArmyHealth[] _spawnableArmyHealths;

        private void Awake()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
            _roadGenerator = FindObjectOfType<RoadGenerator>();
            _levelManager = FindObjectOfType<LevelManager>();
            _balanceCounter = FindObjectOfType<BalanceCounter>();
            Load();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ResetProgress();
        }
         
        private void Start()
        {
            _bossSkinChanger = FindObjectOfType<BossSkinChanger>();
            _spawnableArmyHealths = FindObjectsOfType<SpawnableArmyHealth>();
            LoadValuesOfSpawnedObjects();
        }

        public void Save()
        {
            var spawnedTypes = _spawnManager.GetTypesOfSpawnedObjects();

            for (int i = 0; i < spawnedTypes.Length; i++)
            {
                YandexGame.savesData.SpawnableTypes[i] = (int)spawnedTypes[i];
            }

            for (int i = 0; i < _spawnableArmyHealths.Length; i++)
            {
                YandexGame.savesData.SpawnableArmyMaximumHealths[i] = _spawnableArmyHealths[i].MaximumStartHealth;
                YandexGame.savesData.SpawnableArmyMinimumHealths[i] = _spawnableArmyHealths[i].MinimumStartHealth;
            }

            YandexGame.savesData.IsLevelRestarted = _levelManager.IsLevelRestarted;
            YandexGame.savesData.CurrentBossIndex = _bossSkinChanger.CurrentBossIndex;
            YandexGame.savesData.Balance = _balanceCounter.Balance;

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
            _balanceCounter.Balance = YandexGame.savesData.Balance;           
        }

        private void LoadValuesOfSpawnedObjects()
        {
            _bossSkinChanger.CurrentBossIndex = YandexGame.savesData.CurrentBossIndex;

            for (int i = 0; i < _spawnableArmyHealths.Length; i++)
            {
                _spawnableArmyHealths[i].MaximumStartHealth = YandexGame.savesData.SpawnableArmyMaximumHealths[i];
                _spawnableArmyHealths[i].MinimumStartHealth = YandexGame.savesData.SpawnableArmyMinimumHealths[i];
            }
        }

        private void ResetProgress()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }

        private void OnEnable()
        {
            _levelManager.SceneRestarted += Save;
        }

        private void OnDisable()
        {
            _levelManager.SceneRestarted -= Save;
        }
    }
}
