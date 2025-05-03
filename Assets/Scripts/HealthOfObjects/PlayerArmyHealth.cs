using UnityEngine;
using YG;

namespace HealthOfObjects
{
    public class PlayerArmyHealth : ArmyHealth
    {
        [SerializeField] private int _rewardId;

        private void Revive(int id)
        {
            if (id != _rewardId)
                return;

            AddHealth(_startHealth);
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Revive;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Revive;
        }
    }
}

