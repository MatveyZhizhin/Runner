using System;
using UI;
using UnityEngine;


namespace skin
{
    public class DisplaySkinPrice : MonoBehaviour, ITextUser
    {
        public event Action<string> Changed;
        public void Display(int price)
        {
            Changed?.Invoke(price.ToString());
        }
    }
}
