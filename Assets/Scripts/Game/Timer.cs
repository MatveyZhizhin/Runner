using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _startTime;
        private float _time;

        private bool _isStarted;

        public event Action Started;
        public event Action<float> Updated;
        public event Action Ended;
        [SerializeField] private UnityEvent OnTimerEnd;

        private void Start()
        {
            _time = _startTime;
        }

        private void Update()
        {
            if (_isStarted)
            {
                if (_time <= 0)
                {
                    EndTimer();
                    _time = _startTime;
                }
                else
                {
                    _time -= Time.deltaTime;
                    Updated?.Invoke(_time / _startTime);
                }
            }
        }

        public void StartTimer()
        {
            Started?.Invoke();
            _isStarted = true;
        }
        public void EndTimer()
        {
            Ended?.Invoke();
            OnTimerEnd?.Invoke();
            _isStarted = false;
        }
    }
}
