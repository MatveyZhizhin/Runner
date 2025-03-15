using System;
using UI;
using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour, ITextUser
    {
        [SerializeField] private long _balance;
        [SerializeField] private int _levelBalance;

        private long _startBalance;


        public event Action<string> Changed;


        private void Start()
        {
            IncreaseBalance(_startBalance);
        }

        public void IncreaseLevelBalance(int value)
        {
            _levelBalance += value;
        }

        private void IncreaseBalance(long howMuchWillTheBalanceIncrease)
        {
            _balance += howMuchWillTheBalanceIncrease;
            Changed?.Invoke(_balance.ToString());
        }

        public void DecreaseBalance(long count)
        {
            if (count > _balance) return;
         
            _balance -= count;
            Changed?.Invoke(_balance.ToString());        
        }     
    }
}