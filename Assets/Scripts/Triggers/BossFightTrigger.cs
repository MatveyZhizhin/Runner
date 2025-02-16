using Army.PlayerArmy;
using Army.Units;

namespace Triggers
{
    public class BossFightTrigger : Trigger<PlayerArmyMover>
    {
        private BossAttack _bossAttack;

        private void Awake()
        {
            _bossAttack = FindObjectOfType<BossAttack>();
        }

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            StartCoroutine(_bossAttack.Attack());
            triggered.TrafficStop();
        }
    }
}
