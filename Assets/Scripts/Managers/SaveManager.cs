using Balance;
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

        [SerializeField] private BossSkinChanger _bossSkinChanger;

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

        public void SetBossSkinChanger(BossSkinChanger bossSkinChanger)
        {
            _bossSkinChanger = bossSkinChanger;
        }

        public void Save()
        {
            var spawnedTypes = _spawnManager.GetTypesOfSpawnedObjects();

            for (int i = 0; i < spawnedTypes.Length; i++)
            {
                YandexGame.savesData.SpawnableTypes[i] = (int)spawnedTypes[i];
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
            _bossSkinChanger.CurrentBossIndex = YandexGame.savesData.CurrentBossIndex;
            _balanceCounter.Balance = YandexGame.savesData.Balance;
        }

        private void ResetProgress()
        {
            _levelManager.IsLevelRestarted = false;
            YandexGame.ResetSaveProgress();
            Save();
        }
    }
}
