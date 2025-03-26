using SkinChangers;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public int CurrentLevel { get; set; } = 1;
        [SerializeField] private UnityEvent OnLevelStart;
        [SerializeField] private UnityEvent OnLevelRestart;
        [SerializeField] private BossSkinChanger _bossSkinChanger;

        private bool _isLevelRestarted = true;

        public bool IsLevelRestarted { get => _isLevelRestarted; set => _isLevelRestarted = value; }

        public bool IsBossFightStarted { get; set; }

        public event Action<bool> LevelChanged;

        private SaveManager _saveManager;

        private void Start()
        {
            _isLevelRestarted = true;
            _saveManager.Save();
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
                _bossSkinChanger.ChangeCurrentBossIndex();
            }

            LevelChanged?.Invoke(isBossDead);

            _isLevelRestarted = false;
            _saveManager.Save();
        }

        public void StartLevel()
        {
            OnLevelStart?.Invoke();
            _saveManager.Save();
        }

        public void RestartLevel()
        {
            if (IsBossFightStarted)
                return;

            _saveManager.Save();
            OnLevelRestart?.Invoke();
            Time.timeScale = 0f;          
        }

        public void RestartScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}