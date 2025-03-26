using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelEndPanelView : MonoBehaviour
    {
        [SerializeField] private Image _levelEndPanel;
        [SerializeField] private Image _bossDeathPanel;

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void EnablePanel(bool isBossDead)
        {
            if (isBossDead)
            {
                _bossDeathPanel.gameObject.SetActive(true);
                return;
            }
            
            _levelEndPanel.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _levelManager.LevelChanged += EnablePanel;
        }

        private void OnDisable()
        {
            _levelManager.LevelChanged -= EnablePanel;
        }
    }
}
