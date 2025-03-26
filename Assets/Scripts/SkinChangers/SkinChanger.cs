using UnityEngine;

namespace SkinChangers
{
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] protected GameObject[] _skins;     

        protected void ChangeSkin(int index)
        {
            for (int i = 0; i < _skins.Length; i++)
            {
                if (_skins[i].activeInHierarchy)
                {
                    _skins[i].SetActive(false);
                }

                _skins[index].SetActive(true);
            }
        }
    }
}
