using Managers;
using UnityEngine;

namespace SkinChangers
{
    public class BossSkinChanger : SkinChanger
    {
        private int _currentBossIndex;

        public int CurrentBossIndex { get => _currentBossIndex; set => _currentBossIndex = value; }

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            ChangeSkin(_currentBossIndex);
        }

        public void ChangeCurrentBossIndex()
        {
            var lastBossIndex = _currentBossIndex;

            while (lastBossIndex == _currentBossIndex)
                _currentBossIndex = Random.Range(0, _skins.Length - 1);
        }

        private void OnEnable()
        {
            _levelManager.BossDead += ChangeCurrentBossIndex;
        }

        private void OnDisable()
        {
            _levelManager.BossDead -= ChangeCurrentBossIndex;
        }
    }
}
