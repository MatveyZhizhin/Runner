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
            IncreaseBalance(0);
        }

        public void IncreaseBalance(int count)
        {
            _balance += count;
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