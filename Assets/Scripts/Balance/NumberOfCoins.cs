using UnityEngine;

public class NumberOfCoins : MonoBehaviour
{
    [SerializeField] private int _howMuchWillTheBalanceIncrease;

    public int HowMuchWillTheBalanceIncrease { get => _howMuchWillTheBalanceIncrease; set => _howMuchWillTheBalanceIncrease = value; }
}
