using System;
using UI;
using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour, ITextUser
    {
        [SerializeField] private long _balance;
        public event Action<string> Changed;


        private void Start()
        {
            BalanceIncrease(0);
        }

        public void BalanceIncrease(int count)
        {
            _balance += count;
            Changed?.Invoke(_balance.ToString());
        }

        public void BalanceReduction(int count)
        {
            if (count > _balance) return;
         
            _balance -= count;
            Changed?.Invoke(_balance.ToString());
            
        }
        
        
    }
}