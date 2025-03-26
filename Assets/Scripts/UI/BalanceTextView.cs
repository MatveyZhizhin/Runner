using Balance;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(BalanceCounter))]
    public class BalanceTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _levelBalanceText;

        private BalanceCounter _balanceCounter;

        private void Awake()
        {
            TryGetComponent(out _balanceCounter);
        }

        private void ChangeBalanceText(string text)
        {
            _balanceText.SetText(text);
        }

        private void ChangeLevelBalanceText(string text)
        {
            _levelBalanceText.SetText(text);
        }

        private void OnEnable()
        {
            _balanceCounter.BalanceChanged += ChangeBalanceText;
            _balanceCounter.LevelBalanceChanged += ChangeLevelBalanceText;
        }

        private void OnDisable()
        {
            _balanceCounter.BalanceChanged -= ChangeBalanceText;
            _balanceCounter.LevelBalanceChanged -= ChangeLevelBalanceText;
        }
    }
}
