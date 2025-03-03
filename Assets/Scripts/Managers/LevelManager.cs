using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public int CurrentLevel { get; set; } = 1;
        [SerializeField] private int _mainSceneIndex = 0;

        private bool _isLevelRestarted;
        public bool IsLevelRestarted { get => _isLevelRestarted; set => _isLevelRestarted = value; }

        private SaveManager _saveManager;

        private void Awake()
        {
            _saveManager = FindObjectOfType<SaveManager>();
        }

        public void ChangeLevel()
        {
            CurrentLevel++;
        }

        public void RestartLevel()
        {
            _isLevelRestarted = true;
            _saveManager.Save();
            SceneManager.LoadScene(_mainSceneIndex);
        }
    }
}