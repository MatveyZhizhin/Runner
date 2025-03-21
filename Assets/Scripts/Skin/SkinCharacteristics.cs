using System.Xml.Serialization;
using UnityEngine;


namespace Skin
{
    public class SkinCharacteristics : MonoBehaviour
    {
        [SerializeField] private bool _isForSale;
        [SerializeField] private bool _isSelected;
        [SerializeField] private int _price;

        public bool GetIsForSale() { return _isForSale; }
        public bool GetIsSelected() { return _isSelected; }

        public void SetIsSelected(bool state) {  _isSelected = state; }
        public void Buy() { _isForSale = true; }

        public void SetActiveTrue()
        {
            gameObject.SetActive(true);
        }
        public void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }


        public int GetPrice() { return _price; }


    }
}