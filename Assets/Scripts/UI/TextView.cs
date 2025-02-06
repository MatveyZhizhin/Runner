using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(ITextUser))]
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ITextUser _textUser;

        private void Awake()
        {
            TryGetComponent(out _textUser);
        }

        private void DisplayText(string text)
        {
            _text.SetText(text);
        }

        private void OnEnable()
        {
            _textUser.Changed += DisplayText;
        }

        private void OnDisable()
        {
            _textUser.Changed -= DisplayText;
        }
    }
}