using Army.PlayerArmy;
using Balance;

namespace Triggers
{
    public class MoneyTrigger : Trigger<PlayerArmyMover>
    {
        private BalanceCounter _balance;

        private void Awake()
        {
            _balance = FindFirstObjectByType<BalanceCounter>();
        }

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            _balance.IncreaseBalance();
            gameObject.SetActive(false);
        }
    }
}