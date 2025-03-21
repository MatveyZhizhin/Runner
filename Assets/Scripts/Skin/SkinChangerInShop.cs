using Balance;
using skin;
using System.Net.WebSockets;
using UnityEngine;

namespace Skin
{
    public class SkinChangerInShop: MonoBehaviour
    {
        [SerializeField] private SkinCharacteristics[] _skins;
        [SerializeField] private int _whatSkinIsChosen;

        private BalanceCounter _balance;
        private DisplaySkinPrice _price;
        private BuyButton _buyButton;

        private void Awake()
        {
            _balance = FindObjectOfType<BalanceCounter>();
            _price = FindObjectOfType<DisplaySkinPrice>();
            _buyButton = FindObjectOfType<BuyButton>();
        }

        private void Start()
        {
            EnableSkinOnStartup();
        }

        public void SwitchToNextSkin()
        {
            if (_whatSkinIsChosen < _skins.Length - 1)
            {
                _skins[_whatSkinIsChosen].SetActiveFalse();
                _whatSkinIsChosen++;
                EnableSkin();


            }
            else
            {
                _skins[_whatSkinIsChosen].SetActiveFalse();
                _whatSkinIsChosen = 0;
                EnableSkin();


            }
        }
        public void SwitchSkinToPrevious()
        {
            if (_whatSkinIsChosen != 0)
            {
                _skins[_whatSkinIsChosen].SetActiveFalse();
                _whatSkinIsChosen--;
                EnableSkin();


            }
            else
            {
                _skins[_whatSkinIsChosen].SetActiveFalse();
                _whatSkinIsChosen = _skins.Length - 1;
                EnableSkin();
            }
        }


        private void EnableSkin()
        {
            _skins[_whatSkinIsChosen].SetActiveTrue();
            TurningOnTheDesiredButton(_skins[_whatSkinIsChosen].GetIsForSale());
        }

        private void TurningOnTheDesiredButton(bool isForSale)
        {
            if (!isForSale)
            {
                _buyButton.EnableBuyButton();
                _price.Display(_skins[_whatSkinIsChosen].GetPrice());
            }
            else
            {
                _buyButton.EnableSelectedButton();
            }

        }

        public void BuySkin()
        {
            if (_balance.GetBalance() >= _skins[_whatSkinIsChosen].GetPrice())
            {
                _balance.DecreaseBalance(_skins[_whatSkinIsChosen].GetPrice());
                _skins[_whatSkinIsChosen].Buy();
                TurningOnTheDesiredButton(_skins[_whatSkinIsChosen].GetIsForSale());

            }
        }

        public void SelectedSkin()
        {
            _skins[_whatSkinIsChosen].SetIsSelected(true);
            foreach (var skin in _skins)
            {
                if (skin != _skins[_whatSkinIsChosen])
                {
                    skin.SetIsSelected(false);
                }
            }
        }
        private void EnableSkinOnStartup()
        {
            foreach (var skin in _skins)
            {
                if (skin.GetIsSelected())
                {
                    skin.SetActiveTrue();
                    break;
                }
                else
                {
                    skin.SetActiveFalse();
                    skin.SetIsSelected(false);
                }
                _whatSkinIsChosen++;
            }
        }
    }
}