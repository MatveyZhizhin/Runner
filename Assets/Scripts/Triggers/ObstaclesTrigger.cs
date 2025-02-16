using Army.PlayerArmy;
using HealthOfObjects;
using UnityEngine;


namespace Triggers
{
    public class ObstaclesTrigger : Trigger<PlayerArmyMover>
    {
       

        protected override void OnEnter(PlayerArmyMover triggered)
        {
            triggered.TryGetComponent(out ArmyHealth armyHealth);
            armyHealth.TakeDamage(100);
            
            StartCoroutine(triggered.Pushing());
        }
    }
}