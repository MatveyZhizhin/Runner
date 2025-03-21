using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _selectedButton;

    public void EnableBuyButton()
    {
        _selectedButton.SetActive(false);
        _buyButton.SetActive(true);
    }
    public void EnableSelectedButton()
    {
        _buyButton.SetActive(false);
        _selectedButton.SetActive(true);
    }
}
