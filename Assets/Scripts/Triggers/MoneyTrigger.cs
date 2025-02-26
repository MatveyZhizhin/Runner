using Army.PlayerArmy;
using Balance;
using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(NumberOfCoinsWhenPickingUp))]
    public class MoneyTrigger : Trigger<PlayerArmyMover>
    {
        private BalanceCounter _balance;
        private NumberOfCoinsWhenPickingUp _howMuchWillTheBalanceIncrease;

        private void Awake()
        {
            _balance = FindFirstObjectByType<BalanceCounter>();
            TryGetComponent(out _howMuchWillTheBalanceIncrease);
        }

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            _balance.IncreaseBalance(_howMuchWillTheBalanceIncrease.Get());
            gameObject.SetActive(false);
        }
    }
}