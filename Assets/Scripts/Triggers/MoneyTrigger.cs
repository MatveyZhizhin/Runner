using Army.PlayerArmy;
using Balance;
using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(NumberOfCoins))]
    public class MoneyTrigger : Trigger<PlayerArmyMover>
    {
        private BalanceCounter _balance;
        private NumberOfCoins _howMuchWillTheBalanceIncrease;

        private void Awake()
        {
            _balance = FindObjectOfType<BalanceCounter>();
            TryGetComponent(out _howMuchWillTheBalanceIncrease);
        }

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            _balance.IncreaseLevelBalance(_howMuchWillTheBalanceIncrease.HowMuchWillTheBalanceIncrease);
            gameObject.SetActive(false);
        }
    }
}