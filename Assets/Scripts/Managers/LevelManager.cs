using Army.Units;
using HealthOfObjects;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public int CurrentLevel { get; set; } = 1;
        [SerializeField] private UnityEvent OnLevelRestart;
        

        private bool _isLevelRestarted = true;

        public bool IsLevelRestarted { get => _isLevelRestarted; set => _isLevelRestarted = value; }

        public bool IsBossFightStarted { get; set; }

        [SerializeField] private int _bossAdditionalHealth;
        [SerializeField] private int _bossAdditionalDamage;
        [SerializeField] private BossAttack _boss;

        public event Action<bool> LevelChanged;
        public event Action SceneRestarted;
        public event Action BossDead;

        public void ChangeLevel(bool isBossDead)
        {
            if (!IsBossFightStarted)
                return;

            CurrentLevel++;

            if (isBossDead)
            {
                BossDead?.Invoke();
                _boss.Damage += _bossAdditionalDamage;
                _boss.GetComponent<Health>().AddHealth(_bossAdditionalHealth);
            }

            LevelChanged?.Invoke(isBossDead);

            _isLevelRestarted = false;
        }

        public void RestartLevel()
        {
            if (IsBossFightStarted)
                return;

            OnLevelRestart?.Invoke();         
        }

        public void RestartScene(int index)
        {
            SceneRestarted?.Invoke();
            SceneManager.LoadScene(index);
        }
    }
}