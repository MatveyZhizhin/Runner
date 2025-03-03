using HealthOfObjects;
using UnityEngine;



namespace Triggers
{
    [RequireComponent(typeof(WhatPercentageOfThePlayerArmyStartingHealthWillBeRestored))]
    public class FirstAidKitsTrigger : Trigger<ArmyHealth>
    {
        private WhatPercentageOfThePlayerArmyStartingHealthWillBeRestored _percentageOfTreatment;

        private void Awake()
        {
            TryGetComponent(out _percentageOfTreatment);
        }

        protected override void OnEnter(ArmyHealth triggered)
        {
            triggered.Heal(_percentageOfTreatment.GetPercent());
            gameObject.SetActive(false);
        }
    }
}