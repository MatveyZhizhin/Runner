using Army.PlayerArmy;
using Army.Units;
using Managers;

namespace Triggers
{
    public class BossFightTrigger : Trigger<PlayerArmyMover>
    {
        private BossAttack _bossAttack;

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            _bossAttack = FindObjectOfType<BossAttack>();
        }

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            StartCoroutine(_bossAttack.Attack());
            _levelManager.IsBossFightStarted = true;
        }
    }
}
