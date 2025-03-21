using System;
using UI;
using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour, ITextUser
    {
        [SerializeField] private int _balance;
        public event Action<string> Changed;


        private void Start()
        {
            IncreaseBalance(0);  //когда сделаешь сохранение допиши сюда вызов числа из сохранёных
        }

        public void IncreaseBalance(int howMuchWillTheBalanceIncrease)
        {
            _balance += howMuchWillTheBalanceIncrease;
            Changed?.Invoke(_balance.ToString());
        }

        public void DecreaseBalance(int count)
        {
            if (count >= _balance) return;
         
            _balance -= count;
            Changed?.Invoke(_balance.ToString());        
        }     

        public int GetBalance()
        {
            return _balance;
        }
    }
}