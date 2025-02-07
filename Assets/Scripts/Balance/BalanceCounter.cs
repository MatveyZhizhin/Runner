using UnityEngine;

namespace Balance
{ 
    public class BalanceCounter : MonoBehaviour
    {
        [SerializeField] private long _balance;


        public void BalanceIncrease(int count)
        {
            _balance += count;
        }

        public void BalanceReduction(int count)
        {
            _balance -= count;
        }
    }
}