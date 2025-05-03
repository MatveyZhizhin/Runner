using Managers;
using System;
using UnityEngine;
using YG;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour
    {
        [SerializeField] private long _balance;
        [SerializeField] private int _levelBalance;

        [SerializeField] private int _rewardId;

        public long Balance { get => _balance; set => _balance = value; }

        public event Action<string> BalanceChanged;
        public event Action<string> LevelBalanceChanged;

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            IncreaseBalance(_balance);
            IncreaseLevelBalance(_levelBalance);
        }

        public void IncreaseLevelBalance(int value)
        {
            _levelBalance += value;
            LevelBalanceChanged?.Invoke(_levelBalance.ToString());
        }

        private void IncreaseBalance(long howMuchWillTheBalanceIncrease)
        {
            _balance += howMuchWillTheBalanceIncrease;
            BalanceChanged?.Invoke(_balance.ToString());
        }

        private void IncreaseBalance()
        {
            IncreaseBalance(_levelBalance);
        }

        public void DecreaseBalance(long count)
        {
            if (count > _balance) return;
         
            _balance -= count;
            BalanceChanged?.Invoke(_balance.ToString());        
        }

        private void DoubleLevelBalance(int id)
        {
            if (id != _rewardId)
                return;

            _levelBalance *= 2;
        }

        private void OnEnable()
        {
            _levelManager.SceneRestarted += IncreaseBalance;
            YandexGame.RewardVideoEvent += DoubleLevelBalance;
        }

        private void OnDisable()
        {
            _levelManager.SceneRestarted -= IncreaseBalance;
            YandexGame.RewardVideoEvent -= DoubleLevelBalance;
        }
    }
}