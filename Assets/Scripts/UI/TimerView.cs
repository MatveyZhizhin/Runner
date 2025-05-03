using UnityEngine;
using UnityEngine.UI;
using Game;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Timer))]
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Image _timerImage;
        private Timer _timer;

        private void Awake()
        {
            TryGetComponent(out _timer);
        }

        private void Start()
        {
            DisableTimer();
        }

        private void RenderTimer(float time)
        {
            _timerImage.fillAmount = time;
        }

        private void EnableTimer()
        {
            _timerImage.gameObject.SetActive(true);
        }

        private void DisableTimer()
        {
            _timerImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _timer.Started -= EnableTimer;
            _timer.Ended += DisableTimer;
            _timer.Updated += RenderTimer;
        }

        private void OnDisable()
        {
            _timer.Started += EnableTimer;
            _timer.Ended -= DisableTimer;
            _timer.Updated -= RenderTimer;
        }
    }
}
