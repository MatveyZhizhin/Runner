using UnityEngine;

public class NumberOfCoinsWhenPickingUp : MonoBehaviour
{
    [SerializeField] private int _howMuchWillTheBalanceIncreasep;

    public int get()
    {
        return _howMuchWillTheBalanceIncreasep;
    }
}
