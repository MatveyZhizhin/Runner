using UnityEngine;

namespace Button
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private GameObject _gameMenu;
        [SerializeField] private GameObject _shopMenu;

        public void OpeningOfTheShop()
        {
            _gameMenu.SetActive(false);
            _shopMenu.SetActive(true);
        }
        public void CloseingOfTheShop()
        {
            _gameMenu.SetActive(true);
            _shopMenu.SetActive(false);
        }
    }
}
