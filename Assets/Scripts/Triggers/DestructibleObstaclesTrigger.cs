using HealthOfObjects;
using Triggers;


public class DestructibleObstaclesTrigger : Trigger<ArmyHealth>
{
    private Health _health;

    private void Awake()
    {
        TryGetComponent(out _health);
    }

    protected override void OnEnter(ArmyHealth triggered)
    {
        triggered.TakeDamage(_health.CurrentHealth);
        _health.TakeDamage(_health.CurrentHealth);
    }
}
