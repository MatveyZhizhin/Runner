using Army.PlayerArmy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public int CurrentLevel { get; set; } = 1;
        [SerializeField] private int _mainSceneIndex = 0;
        [SerializeField] private UnityEvent OnLevelChange;
        [SerializeField] private UnityEvent OnLevelStart;

        [SerializeField] private GameObject[] _bosses;

        private bool _isLevelRestarted;
        private int _currentBossIndex = 0;

        public bool IsLevelRestarted { get => _isLevelRestarted; set => _isLevelRestarted = value; }

        public bool IsBossFightStarted { get; set; }

        private SaveManager _saveManager; 

        private void Start()
        {
            //ChangeBoss(_currentBossIndex);
        }

        private void Awake()
        {
            _saveManager = FindObjectOfType<SaveManager>();
        }

        public void ChangeLevel(bool isBossDead)
        {
            if (!IsBossFightStarted)
                return;

            CurrentLevel++;

            if (isBossDead)
            {
                _currentBossIndex = Random.Range(0, _bosses.Length - 1);
            }

            OnLevelChange?.Invoke();

            _isLevelRestarted = false;
            _saveManager.Save();
        }

        public void StartLevel()
        {
            OnLevelStart?.Invoke();
            _isLevelRestarted = true;
            _saveManager.Save();
        }

        private void ChangeBoss(int index)
        {
            foreach (var boss in _bosses)
            {
                if (boss.activeInHierarchy)
                {
                    boss.SetActive(false);
                    _bosses[index].SetActive(true);
                    break;
                }
            }
        }


        public void RestartLevel()
        {
            _isLevelRestarted = true;
            _saveManager.Save();
            SceneManager.LoadScene(_mainSceneIndex);
        }
    }
}