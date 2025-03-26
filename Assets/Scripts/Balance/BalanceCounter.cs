using System;
using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour
    {
        [SerializeField] private long _balance;
        [SerializeField] private int _levelBalance;

        public long Balance { get => _balance; set => _balance = value; }

        public event Action<string> BalanceChanged;
        public event Action<string> LevelBalanceChanged;

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

        public void DecreaseBalance(long count)
        {
            if (count > _balance) return;
         
            _balance -= count;
            BalanceChanged?.Invoke(_balance.ToString());        
        }
    }
}