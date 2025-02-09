using HealthOfObjects;

namespace Triggers
{
    public class NeutralArmyTrigger : Trigger<ArmyHealth>
    {
        private SpawnableArmyHealth _armyHealth;

        private void Awake()
        {
            TryGetComponent(out _armyHealth);
        }

        protected override void OnEnter(ArmyHealth triggered)
        {
            triggered.AddHealth(_armyHealth.CurrentHealth);
            _armyHealth.TakeDamage(_armyHealth.CurrentHealth);
        }
    }
}
