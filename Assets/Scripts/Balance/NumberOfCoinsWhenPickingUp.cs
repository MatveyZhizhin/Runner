using UnityEngine;

public class NumberOfCoinsWhenPickingUp : MonoBehaviour
{
    [SerializeField] private int _howMuchWillTheBalanceIncreasep;

    public int Get()
    {
        return _howMuchWillTheBalanceIncreasep;
    }
}
