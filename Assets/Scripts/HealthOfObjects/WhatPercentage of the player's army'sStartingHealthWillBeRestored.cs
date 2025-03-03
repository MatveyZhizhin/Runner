using UnityEngine;

public class WhatPercentageOfThePlayerArmyStartingHealthWillBeRestored : MonoBehaviour
{
    [SerializeField] private int _percent;

    public int GetPercent()
    {
        return _percent;
    }
}
