using System;
using UI;
using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour, ITextUser
    {
        [SerializeField] private long _balance;
        [SerializeField] private long _howMuchWillTheBalanceIncrease;
        public event Action<string> Changed;


        private void Start()
        {
            IncreaseBalance();
        }

        public void IncreaseBalance()
        {
            _balance += _howMuchWillTheBalanceIncrease;
            Changed?.Invoke(_balance.ToString());
        }

        public void DecreaseBalance(int count)
        {
            if (count > _balance) return;
         
            _balance -= count;
            Changed?.Invoke(_balance.ToString());        
        }     
    }
}